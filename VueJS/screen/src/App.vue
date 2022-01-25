<template>
  <div id="app">
    <side-menu></side-menu>
    <workspace></workspace>
  </div>
</template>

<script>
import SideMenu from './components/SideMenu.vue';
import Workspace from './components/Workspace.vue';

export default {
  name: 'App',
  components: {
    SideMenu,
    Workspace
  },
  data: function() {
    return {
      message: '',
    }
  },
  sockets:{
    connect: () => {
      console.log('Connected to the server');
    }
  },
  methods : {
    sendMessage(){
      this.$socket.emit('eventFromScreen','Coucou du screen');
    }
  }
}
</script>

<style>
  #app{
    display: flex;
  }

  *{
    box-sizing: border-box;
  }

  html,body, #app{
    padding: 0;
    margin: 0;
    height: 100vh;
  }
</style>
