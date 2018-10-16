<template>
  <div class="container container-top">
    <div class="center">
      <div v-if="ticket.length === 0" class="name warrning">
        Ticket is empty
      </div>
      <template v-else>
      <table class="table">
        <thead>
          <th>Match</th>
          <th>Type</th>
          <th>Odd</th>
          <th></th>
        </thead>
        <tbody>
          <tr v-for="( game, index ) in ticket" :key="index">
            <td>{{ game.name }}</td>
            <td>{{ game.type }}</td>
            <td>{{ game.odd }}</td>
            <span
              @click="remove(game.id)"
              class="material-icons md-24 main-color hover">
              delete
            </span>
          </tr>
        </tbody>
      </table>
      <div class="ticket-bet">
        <span>
          Place stake:
        </span>
        <input v-model="stake" type="text" class="input">
        <button class="button" @click="placeBet">Place bet</button>
      </div>
      </template>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      stake: 5
    };
  },
  computed: {
    ticket() {
      return this.$store.getters.getTicket;
    }
  },
  methods: {
    placeBet() {
      const bet = {
        ticket: this.ticket,
        stake: this.stake
      }
      this.$store.dispatch('placeBet', bet);
    },
    remove(id) {
      this.$store.dispatch('removeFromTicket', id);
    }
  }
};
</script>

<style lang="scss" scoped>
.ticket-bet {

}
</style>

