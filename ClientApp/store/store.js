import Vue from 'vue';
import Vuex from 'vuex';
import createPersistedState from 'vuex-persistedstate';

import axios from 'axios';

Vue.use(Vuex);

const store = new Vuex.Store({
  // plugins: [createPersistedState({
  //   reducer: (persistedState => {
  //     const stateFilter = Object.assign({}, persistedState);
  //     const blackList = ['offer'];
  //     blackList.forEach(item => {
  //       delete stateFilter[item];
  //     });
  //     return stateFilter;
  //   })
  // })],
  state: {
    // get funds from base
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
    resetTicket(state) {
      state.ticket = [];
    },
    addOffer(state, offer) {
      state.offer.push(offer);
    },
    addGameToTicket(state, bet) {
      const indexOnTicket = state.ticket.games.findIndex(e => e.gameId === bet.GameId);
      debugger;
      if (indexOnTicket === -1) {
        return axios.post('/api/ticket/add', bet)
          .then(response => {
            console.log(response.data);
          })
          .catch(err => console.log(err));
      } else {
        // update par na ticket_game
        return axios.put('/api/ticket/update', bet)
          .then(res => {
            console.log(res.data);
            debugger;
          });
      }
    },
    removeGameFromTicket(state, game) {
      console.log(game);
      debugger;
      return axios.delete(`/api/ticket/delete/${game}`)
        .then(response => {
          console.log(response);
          debugger;
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
    placeBet({ commit }, bet) {
      commit('finishTicket', bet);
      // commit('resetTicket');
    },
    getOffer({ commit }, sport) {
      // get offer from db
      axios.get('/api/offer/index')
        .then(response => commit('addOffer', response.data));
    },
    addToTicket({ commit }, bet) {
      console.log(bet);
      commit('addGameToTicket', bet);
    },
    removeFromTicket({ commit }, game) {
      commit('removeGameFromTicket', game);
    }
  }
});

export default store;
