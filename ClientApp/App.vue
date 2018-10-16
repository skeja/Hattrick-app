<template>
  <div class="app">
    <navbar></navbar>
    <transition name="fade" mode="out-in">
      <router-view></router-view>
    </transition>
  </div>
</template>

<script>
import Navbar from './components/Navbar';

export default {
  components: {
    Navbar
  }
};
</script>

<style lang="scss">
@import './style/style.scss';

.app {
  display: flex;
  flex-direction: column;
  // height: 100%;
}
</style>
