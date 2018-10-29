<template>
  <div class="container container-top">
    <loader v-if="showLoader"></loader>
    <div v-else class="center">
      <div v-if="tickets.length === 0 " class="name warrning">
        No tickets
      </div>
      <template>
        <table v-for="(ticket, index) in tickets" :key="index" class="table">
          <thead>
            <th>Match</th>
            <th>Odd</th>
          </thead>
          <tbody>
            <tr v-for="(game, index) in ticket.games" :key="index">
              <td>{{ game.game.name }}</td>
              <td>{{ game.game[game.type] }}</td>
            </tr>
          </tbody>
          <div class="details">
            <span>Odd: {{ ticket.odd }}</span>
            <span>Stake: {{ ticket.stake }}</span>
          </div>
        </table>
      </template>
    </div>
  </div>
</template>

<script>
import Loader from './common/Loader';
import { map } from 'lodash-es';

export default {
  components: {
    Loader
  },
  data() {
    return {
      showLoader: true
    };
  },
  computed: {
    tickets() {
      const tickets = this.$store.getters.getTickets;
      return map(tickets, item => ({
        games: item,
        odd: item[0].ticket.odd,
        stake: item[0].ticket.stake
      }));
    }
  },
  created() {
    this.$store.dispatch('getBetted')
      .then(() => { this.showLoader = false; });
  }
};
</script>

<style lang="scss" scoped>
table {
  margin-bottom: 3rem;
}
.details {
  display: flex;
  // flex-direction: column;
  padding: 1rem;
  justify-content: space-around;
  margin-top: 1rem;
  font-weight: 500;

  &span {

  }
}
</style>
