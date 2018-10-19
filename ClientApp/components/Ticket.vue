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
          <tr v-for="( game, index ) in ticket.games" :key="index">
            <td>{{ game.game.name }}</td>
            <td>{{ game.type }}</td>
            <td>{{ game.game[game.type] }}</td>
            <span
              @click="remove(game)"
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
        <span>Odd: {{ odd }}</span>
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
  created() {
    this.$store.dispatch('findOrCreateTicket');
  },
  computed: {
    ticket() {
      return this.$store.getters.getTicket;
    },
    bonus() {
      return this.$store.getters.getBonus
    },
    odd() {
      const ticket = this.$store.getters.getTicket;
      this.$store.dispatch('getBonus', ticket.id);
      // const bonus = this.$store.getters.getBonus;
      let odd = 0;
      ticket.games.forEach(e => {
        odd += e.game[e.type];
      });
      odd += this.bonus;
      return Math.floor(odd);
    }
  },
  methods: {
    placeBet() {
      this.$store.dispatch('placeBet', { ticket: this.ticket, stake: this.stake, odd: this.odd });
    },
    remove(game) {
      const ticket = {
        TicketId: game.ticketId,
        GameId: game.gameId,
        Type: game.type
      };
      this.$store.dispatch('removeFromTicket', ticket);
    }
  }
};
</script>
