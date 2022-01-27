<template>
  <div class="workspace-container">
    <div>
      Message: {{ msg }}
      <input type="number" v-model="bpm">
      <button @click="sendBPM()">Send new BPM</button>
    </div>
    <div class="color-picker">
      <input type="color" name="color" id="color" v-model="color">
      {{color}}
      <button @click="sendColor()">Send new color</button>
    </div>
    <div class="tracks">
      <Track></Track>
    </div>
    <av-waveform id="la" audio-src="sonoiseaux.mp3"></av-waveform>
  </div>
</template>

<script>
import Track from './Track.vue';

export default {
  name: "Workspace",
  components:{
    Track
  },
  data(){
    return {
      msg: '',
      bpm: 0,
      color: '#000000'
    }
  },
  sockets:{},
  methods:{
    sendBPM(){
      console.log('Send BPM');
      this.$socket.emit('newBPM',this.bpm);
    },
    sendColor(){
      console.log('Send color', this.hexToRGB(this.color));
      this.$socket.emit('changeLights',this.color)
    },
    hexToRGB(h){
      let r = 0, g = 0, b = 0;

      // 3 digits
      if (h.length == 4) {
        r = "0x" + h[1] + h[1];
        g = "0x" + h[2] + h[2];
        b = "0x" + h[3] + h[3];

      // 6 digits
      } else if (h.length == 7) {
        r = "0x" + h[1] + h[2];
        g = "0x" + h[3] + h[4];
        b = "0x" + h[5] + h[6];
      }
      
      return {r: +r, g: +g, b: +b};
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
  .workspace-container{
    width: 100%;
    overflow: hidden;
  }

  .color-picker{
    color: white;
  }

  .tracks{
    min-height: 70%;
    background-color: #535353;
    padding: 1.5em 1em;
    overflow-x: scroll;
  }

  .tracks::-webkit-scrollbar{
    height: 10px;
  }

  .tracks::-webkit-scrollbar-thumb{
    background-color: #ffffff13;
    border-radius: 10px;
    height: 8px;
  }
</style>
