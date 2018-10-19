import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    funds: 0,
    offer: [],
    ticket: {},
    tickets: []
  },
  getters: {
    getFunds(state) {
      return state.funds;
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
    getFunds(state) {
      return axios.get('/api/wallet/index')
        .then(({ data }) => {
          state.funds = data.funds;
        });
    },
    finishTicket(state, bet) {
      debugger;
      if ((state.funds -= bet) < 0) {
        return 'Funds low!!';
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
    resetTicket(state) {
      state.ticket = [];
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
    findOrCreate(state, payload) {
      axios.get('/api/ticket/last')
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
      commit('findOrCreate');
    },
    updateFunds(dispatch, funds) {
      return axios.put('/api/wallet/updateFunds', funds)
        .then(response => {
          debugger;
        });
    },
    placeBet({ dispatch, commit, getters }, bet) {
      // commit('finishTicket', bet);
      let funds = getters.getFunds;
      debugger;
      if ((funds -= bet) < 0) {
        return 'Funds low!!';
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
          commit('findOrCreate');
          dispatch('updateFunds', bet.funds);
        });
    },
    getOffer({ commit }, sport) {
      // get offer from db
      axios.get('/api/offer/index')
        .then(response => commit('addOffer', response.data));
    },
    addToTicket({ commit, getters }, bet) {
      const ticket = getters.getTicket;
      const indexOnTicket = ticket.games.findIndex(e => e.gameId === bet.GameId);
      if (indexOnTicket === -1) {
        return axios.post('/api/ticket/add', bet)
          .then(response => {
            commit('findOrCreate');
          })
          .catch(err => console.log(err));
      } else {
        // update par na ticket_game
        return axios.put('/api/ticket/updateGame', bet)
          .then(res => {
            commit('findOrCreate');
          });
      }
    },
    removeFromTicket({ commit }, game) {
      return axios.delete(`/api/ticket/delete/${game.TicketId}/${game.GameId}`)
        .then(response => {
          commit('findOrCreate');
        })
        .catch(err => console.log(err));
    }
  }
});

export default store;
