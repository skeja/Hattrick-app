<template>
  <div class="container container-top">
    <div class="center">
      <table v-for="(sport, index) in offer" :key="index" class="table">
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
              <td @click="addGame(game, '1')">{{ game[1] }}</td>
              <td @click="addGame(game, 'x')">{{ game.x }}</td>
              <td @click="addGame(game, '2')">{{ game[2] }}</td>
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
import { groupBy } from 'lodash-es';
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
      const filteredOffer = [];
      groupBy(offer, o => o.league.sport.name);s
      offer.forEach(sport => {
        filteredOffer.push(groupBy(sport, 'leagueId'));
        debugger;
      });
      return filteredOffer;
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
      const game = JSON.parse(JSON.stringify(data));

      const pair = {
        id: game.id,
        name: game.name,
        type: bet,
        odd: game[bet]
      }
      console.log(pair);
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
