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
            <td>{{ game.name }}</td>
            <td>{{ game.odd }}</td>
          </tr>
        </tbody>
      </table>
    </template>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    tickets() {
      return this.$store.getters.getTickets;
    }
  }
}
</script>

<style lang="scss" scoped>
table {
  margin-bottom: 3rem;
}
</style>
