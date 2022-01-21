<template>
  <div>
    Message: {{ msg }}
    <input type="number" v-model="bpm">
    <button @click="sendBPM()">Send new BPM</button>
    <av-waveform id="la"
        audio-src="sonoiseaux.mp3"
    ></av-waveform>
  </div>
</template>

<script>
export default {
  name: "Workspace",
  data(){
    return {
      msg: '',
      bpm: 0
    }
  },
  sockets:{},
  methods:{
    sendBPM(){
      console.log('Send BPM');
      this.$socket.emit('newBPM',this.bpm);
    }
  },
  created() {
    this.$options.sockets.eventToScreen = (data) => {
      console.log(`Test subscribe: ${data}`);
      this.msg = data;
    };
  },
};
</script>

<style lang="css" scoped>
</style>
