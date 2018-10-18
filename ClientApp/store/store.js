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
    ticket: [],
    ticketId: null,
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
    getTicketId(state) {
      return state.ticketId;
    },
    getTickets(state) {
      return state.tickets;
    }
  },
  mutations: {
    placeBet(state, bet) {
      console.log(state.funds -= bet.ticket);
      if ((state.funds -= bet) < 0) {
        return 'Funds low!!';
      }
      // state.tickets.push(bet);
      // state.funds -= bet;
      // add ticket to db
    },
    resetTicket(state) {
      state.ticket = [];
    },
    addOffer(state, offer) {
      state.offer.push(offer);
    },
    addGameToTicket(state, bet) {
      console.log(bet);
      return axios.post('/api/ticket/add', bet)
        .then(response => {
          console.log(response.data);
        })
        .catch(err => console.log(err));
    },
    removeGameFromTicket(state, id) {
    },
    findOrCreate(state, payload) {
      axios.get('/api/ticket/last')
        .then(({ data }) => {
          debugger;
          state.ticketId = data.id;
          if (data !== '') {
            return axios.get(`/api/ticket/find?Id=${data.id}`)
              .then((res) => {
                debugger;
                state.ticket = res.data;
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
    placeBet({ commit }, ticket) {
      commit('placeBet', ticket);
      commit('resetTicket');
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
    removeFromTicket({ commit }, id) {
      commit('removeGameFromTicket', id);
    }
  }
});

export default store;
