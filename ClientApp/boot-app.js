import Vue from 'vue'
// import App from './components/App'
import App from './App';

// import router from './router'
import router from './router/router';
import store from './store/store'
// import store from './store'
import { sync } from 'vuex-router-sync'

// Sync Vue router and the Vuex store
sync(store, router)

new Vue({ // eslint-disable-line no-new
  el: '#app',
  store,
  router,
  template: '<App/>',
  components: { App }
})
