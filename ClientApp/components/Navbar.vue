<template>
  <nav class="navigation">
    <ul class="horizontal">
      <li
        class="nav-brand">
        <span class="material-icons md-24">home</span>
      </li>
      <router-link
        :to="{ name: 'offer' }"
        tag="li"
        active-class="highlighted">
        Offer
      </router-link>
      <router-link
        :to="{ name: 'tickets' }"
        tag="li"
        active-class="highlighted">
        Tickets
      </router-link>
      <div class="nav-right">
        <li>
          Funds: {{ funds }}
        </li>
        <router-link
          :to="{ name: 'wallet' }"
          tag="li"
          active-class="highlighted">
          Wallet
        </router-link>
      </div>
    </ul>
  </nav>
</template>

<script>
export default {
  data() {
    return {
    };
  },
  computed: {
    funds() {
      return this.$store.getters.getFunds;
    }
  }
};
</script>

<style lang="scss" scoped>
.nav-right {
  display: flex;
  margin-left: auto;
}

.navigation {

  & ul {
    box-shadow: inset 0 -1px 0 0 #18bc9c;
  }
}
</style>
