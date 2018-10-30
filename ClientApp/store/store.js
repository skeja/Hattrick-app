import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';
import { groupBy, map } from 'lodash-es';

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    funds: 0,
    offer: [],
    ticket: {},
    tickets: [],
    bonus: 0
  },
  getters: {
    getFunds(state) {
      return state.funds;
    },
    getBonus(state) {
      return state.bonus;
    },
    getOffer(state) {
      return state.offer;
    },
    getTicket(state) {
      return state.ticket;
    },
    getTickets(state) {
      return state.tickets;
    }
  },
  mutations: {
    setBonus(state, bonus) {
      state.bonus = bonus;
    },
    getFunds(state) {
      return axios.get('/api/wallet/index')
        .then(({ data }) => {
          state.funds = data.funds;
        });
    },
    setFunds(state, funds) {
      state.funds = funds;
    },
    finishTicket(state, bet) {
      if ((state.funds -= bet) < 0) {
        return console.log('Funds low!!');
      }
      // update ticket in db
      const ticket = {
        id: bet.ticket.id,
        isBetted: true
      };
      return axios.put('/api/ticket/updateTicket', ticket)
        .then(response => {
          state.ticket = [];
          console.log(response.data);
        });
    },
    addOffer(state, offer) {
      state.offer.push(offer);
    },
    addBetted(state, tickets) {
      state.tickets = tickets;
    },
    resetTicket(state) {
      state.ticket = {};
    },
    addGameToTicket(state, bet) {
      const indexOnTicket = state.ticket.games.findIndex(e => e.gameId === bet.GameId);
      if (indexOnTicket === -1) {
        return axios.post('/api/ticket/add', bet)
          .then(response => {
            console.log(response.data);
          })
          .catch(err => console.log(err));
      } else {
        // update par na ticket_game
        return axios.put('/api/ticket/updateGame', bet)
          .then(res => {
            console.log(res.data);
          });
      }
    },
    removeGameFromTicket(state, game) {
      return axios.delete(`/api/ticket/delete/${game.TicketId}/${game.GameId}`)
        .then(response => {
        })
        .catch(err => console.log(err));
    },
    addTicket(state, id) {
      state.ticket.id = id;
    },
    addTicketGames(state, games) {
      Vue.set(state.ticket, 'games', games);
    },
    findOrCreate(state, payload) {
      return axios.get('/api/ticket/last')
        .then(({ data }) => {
          state.ticket.id = data.id;
          if (data !== '') {
            return axios.get(`/api/ticket/find?Id=${data.id}`)
              .then((res) => {
                state.ticket.games = res.data;
              })
              .catch(err => console.log(err));
          }
          const ticket = {
            isBetted: false,
            bonusId: 3
          };
          axios.post('/api/ticket/create', ticket)
            .then(response => {
              console.log(response);
            });
        });
    }
  },
  actions: {
    findOrCreateTicket({ commit }) {
      // return commit('findOrCreate');
      return axios.get('/api/ticket/last')
        .then(({ data }) => {
          commit('addTicket', data.id);
          if (data !== '') {
            return axios.get(`/api/ticket/find?Id=${data.id}`)
              .then(res => {
                // state.ticket.games = res.data;
                commit('addTicketGames', res.data);
              })
              .catch(err => console.log(err));
          }
          const ticket = {
            isBetted: false,
            bonusId: 3
          };
          axios.post('/api/ticket/create', ticket)
            .then(response => {
              commit('addTicket', response.data);
            });
        });
    },
    updateFunds({ commit }, stake) {
      return axios.put(`/api/wallet/updateFunds/${stake}`)
        .then(response => {
          // commit('getFunds');
          return axios.get('/api/wallet/index')
            .then(({ data }) => {
              commit('setFunds', data.funds);
            });
        })
        .catch(err => console.log(err));
    },
    placeBet({ dispatch, commit, getters }, bet) {
      // commit('finishTicket', bet);
      let funds = getters.getFunds;
      if ((funds -= bet.stake) < 0) {
        return console.log('Funds low!!');
      }
      // update ticket in db
      const ticket = {
        id: bet.ticket.id,
        stake: bet.stake,
        odd: bet.odd,
        isBetted: true
      };
      return axios.put('/api/ticket/updateTicket', ticket)
        .then(response => {
          commit('resetTicket');
          // commit('findOrCreate');
          dispatch('findOrCreateTicket');
          dispatch('updateFunds', bet.stake);
        });
    },
    getBonus({ commit }, ticketId) {
      return axios.get(`/api/ticket/getBonus?TicketId=${ticketId}`)
        .then(response => {
          commit('setBonus', response.data);
        });
    },
    getOffer({ commit }, sport) {
      // get offer from db
      return axios.get('/api/offer/index')
        .then(response => commit('addOffer', response.data));
    },
    getBetted({ commit }) {
      return axios.get('/api/ticket/getBetted')
        .then(response => {
          const tickets = groupBy(response.data, e => e.ticketId);
          const sortedTickets = [];
          map(tickets, item => {
            const ticket = {
              games: item,
              odd: item[0].ticket.odd,
              stake: item[0].ticket.stake
            };
            sortedTickets.push(ticket);
          });
          commit('addBetted', tickets);
        });
    },
    addToTicket({ dispatch, commit, getters }, bet) {
      const ticket = getters.getTicket;
      const indexOnTicket = ticket.games.findIndex(e => e.gameId === bet.GameId);
      if (indexOnTicket === -1) {
        return axios.post('/api/ticket/add', bet)
          .then(response => {
            dispatch('findOrCreateTicket');
          })
          .catch(err => console.log(err));
      } else {
        // update par na ticket_game
        return axios.put('/api/ticket/updateGame', bet)
          .then(res => {
            dispatch('findOrCreateTicket');
          });
      }
    },
    removeFromTicket({ dispatch, commit }, game) {
      return axios.delete(`/api/ticket/delete/${game.TicketId}/${game.GameId}`)
        .then(response => {
          commit('findOrCreate');
          dispatch('getBonus', game.TicketId);
        })
        .catch(err => console.log(err));
    }
  }
});

export default store;
