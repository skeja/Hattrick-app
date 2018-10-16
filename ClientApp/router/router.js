import Vue from 'vue';
import VueRouter from 'vue-router';

import wallet from '../components/Wallet';
import tickets from '../components/TicketList';
import ticket from '../components/Ticket';
import offer from '../components/Offer';

Vue.use(VueRouter);
const routes = [
  {
    path: '/wallet',
    name: 'wallet',
    component: wallet,
    meta: {
      title: 'Wallet'
    }
  }, {
    path: '/offer',
    name: 'offer',
    component: offer,
    meta: {
      title: 'Offer'
    }
  }, {
    path: '/tickets',
    name: 'tickets',
    component: tickets,
    meta: {
      title: 'Tickets'
    }
  }, {
    path: '/ticket',
    name: 'ticket',
    component: ticket,
    meta: {
      title: 'Ticket'
    }
  },
  { path: '*', redirect: { name: 'offer' } }
];

const router = new VueRouter({ mode: 'history', routes });

router.afterEach((to, from, next) => {
  document.title = to.meta.title;
});

export default router;
