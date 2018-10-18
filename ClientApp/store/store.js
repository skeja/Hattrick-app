import Vue from 'vue';
import Vuex from 'vuex';
import createPersistedState from 'vuex-persistedstate';

import axios from 'axios';

import basketball from '../data/basketballGames.json';
import football from '../data/footballGames.json';
import handball from '../data/handballGames.json';
import hockey from '../data/hockeyGames.json';
import tennis from '../data/tennisGames.json';

Vue.use(Vuex);

const store = new Vuex.Store({
  plugins: [createPersistedState({
    reducer: (persistedState => {
      const stateFilter = Object.assign({}, persistedState);
      const blackList = ['offer'];
      blackList.forEach(item => {
        delete stateFilter[item];
      });
      return stateFilter;
    })
  })],
  state: {
    // get funds from base
    funds: 0,
    offer: [],
    ticket: [],
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
    getFootball(state, payload) {
      state.offer.push(football);
    },
    getBasketball(state, payload) {
      state.offer.push(basketball);
    },
    getHandball(state, payload) {
      state.offer.push(handball);
    },
    getTennis(state, payload) {
      state.offer.push(tennis);
    },
    getHockey(state, payload) {
      state.offer.push(hockey);
    },
    addGameToTicket(state, bet) {
      const indexOnTicket = state.ticket.findIndex(e => e.id === bet.id);
      if (indexOnTicket === -1) {
        state.ticket.push(bet);
      } else {
        state.ticket[indexOnTicket] = bet;
      }
    },
    removeGameFromTicket(state, id) {
    }
  },
  actions: {
    placeBet({ commit }, ticket) {
      commit('placeBet', ticket);
      commit('resetTicket');
    },
    getOffer({ commit }, sport) {
      // get offer from db
      axios.get('/api/offer/index')
        .then(response => commit('addOffer', response.data));
      // commit('getFootball');
      // provjerit koji je sport
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
