import Vue from 'vue'
import App from './App.vue'
import VueSocketIO from 'vue-socket.io'
import SocketIO from 'socket.io-client'
import VueRouter from 'vue-router'
import router from './router'
import {store} from './track.store'

Vue.config.productionTip = false
Vue.use(new VueSocketIO({
  debug: true,
  connection: SocketIO('http://localhost:3000'),
}))
Vue.use(VueRouter)

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app')
