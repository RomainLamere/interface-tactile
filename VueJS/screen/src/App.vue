<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    <button v-on:click="sendMessage">clique</button>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'

export default {
  name: 'App',
  components: {
    HelloWorld
  },
  data: function() {
    return {
      connection: null
    }
  },
  sockets:{
    connect: () => {
      console.log('Connected to the server');
    },
    eventToScreen: (data) => {
      console.log(`Server says : ${data}`);
    }
  },
  methods : {
    sendMessage(){
      this.$socket.emit('eventFromScreen','Coucou du screen')
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
