import Vue from 'vue'
import AudioVisual from 'vue-audio-visual'
import App from './App.vue'
import VueSocketIO from 'vue-socket.io'
import SocketIO from 'socket.io-client'
import VueRouter from 'vue-router'
import router from './router'

Vue.use(AudioVisual)

Vue.config.productionTip = false
Vue.use(new VueSocketIO({
  debug: true,
  connection: SocketIO('http://localhost:3000'),
}))
Vue.use(VueRouter)

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
