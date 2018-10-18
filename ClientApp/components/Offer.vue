<template>
  <div class="container container-top">
    <div class="center">
      <table v-for="(sport, index) in offer[0]" :key="index" class="table">
        <div class="sport">
          {{ index }}
        </div>
        <table v-for="(league, index) in sport" :key="index" class="table">
          <div class="league">
            {{ index }}
          </div>
          <thead>
            <th>Match</th>
            <th>1</th>
            <th>x</th>
            <th>2</th>
          </thead>
          <tbody>
            <tr v-for="(game, index) in league" :key="index">
              <td>{{ game.name }}</td>
              <td @click="addGame(game, 'home')">{{ game.home }}</td>
              <td @click="addGame(game, 'draw')">{{ game.draw }}</td>
              <td @click="addGame(game, 'guest')">{{ game.guest }}</td>
            </tr>
          </tbody>
        </table>
      </table>
    </div>
    <div class="preview">
      <ticket></ticket>
      <button class="button" @click="viewTicket">View ticket</button>
    </div>
  </div>
</template>

<script>
import { groupBy, forEach } from 'lodash-es';
import Ticket from './Ticket.vue';

export default {
  components: {
    Ticket
  },
  data() {
    return {

    };
  },
  computed: {
    offer() {
      const offer = this.$store.getters.getOffer;
      const sortedOffer = [];

      offer.map(o => {
        let data = [];
        data = groupBy(o, e => e.league.sport.name);
        sortedOffer.push(data);
      });
      sortedOffer.forEach(o => {
        forEach(o, (value, key) => {
          o[key] = groupBy(o[key], item => {
            return item.league.name;
          });
        });
      });
      return sortedOffer;
    }
  },
  created() {
    this.$store.dispatch('getOffer');
  },
  methods: {
    viewTicket() {
      this.$router.push({ name: 'ticket' });
    },
    addGame(data, bet) {
      const pair = {
        ticketId: this.$store.getters.getTicket.id,
        gameId: data.id,
        type: bet
      };
      this.$store.dispatch('addToTicket', pair);
    }
  }
};
</script>

<style lang="scss" scoped>
td:hover  {
  background-color: #ddd;
  cursor: pointer;
}
.table {
  margin-bottom: 3rem;
}
// .container {
//   align-items: baseline;
//   flex-direction: row;
// }
.preview {
  max-width: 10rem;
  display: flex;
  flex-direction: column;
  flex-grow: 2;
  margin: 0 2rem;
}
</style>
