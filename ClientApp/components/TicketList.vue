<template>
  <div class="container container-top">
    <div class="center">
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
          <tr v-for="(game, index) in ticket" :key="index">
            <td>{{ game.game.name }}</td>
            <td>{{ game.game[game.type] }}</td>
          </tr>
        </tbody>
      </table>
    </template>
    </div>
  </div>
</template>

<script>
import { groupBy } from 'lodash-es';

export default {
  created() {
    this.$store.dispatch('getBetted');
  },
  computed: {
    tickets() {
      let tickets = this.$store.getters.getTickets;
      tickets = groupBy(tickets, e => e.ticketId);
      return tickets;
    }
  }
};
</script>

<style lang="scss" scoped>
table {
  margin-bottom: 3rem;
}
</style>
