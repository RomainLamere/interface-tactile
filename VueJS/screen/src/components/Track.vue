<template>
  <drop class="drop-zone" @drop="trackDropped">
    <div :class="`instru ${trackZone}`">
      <img
        v-if="instrument !== ''"
        :src="require(`@/assets/icons/${instrument}.png`)"
        alt=""
      />
      <template v-else>
        <div>select track<br>from table</div>
      </template>
    </div>
    <div class="track">
      <button @click="soundControl()" :disabled="!recordAsArrayBuffer">
        {{soundPaused? 'play': 'pause'}}
      </button>
    </div>
  </drop>
</template>
<script>
import { Drop } from "vue-easy-dnd";

export default {
  components: {
    Drop,
  },
  data() {
    return {
      trackZone: '',
      instrument: '',
      recordAsArrayBuffer: null,
      audioContext: new AudioContext(),
      soundPaused: true,
      firstPlay: true
    };
  },
  methods: {
    trackDropped(e) {
      this.$emit('sourceadded');
      this.trackZone = e.data.zone;
      this.instrument = e.data.instrument;
      this.recordAsArrayBuffer = e.data.record;
    },
    playSound(){
      if(this.recordAsArrayBuffer){
        const audioBuffer = this.copyBuff();
        let source = this.audioContext.createBufferSource();
        source.onended = async () => {
          this.audioContext.close().then(() => {
            this.soundPaused = true;
            this.firstPlay = true;
          });
          this.audioContext = new AudioContext();
        };
        this.audioContext.decodeAudioData(audioBuffer, (decoded) =>{
          source.buffer = decoded;
          source.connect(this.audioContext.destination);
          source.start(0);
        });
      }
    },
    soundControl(){
      console.log(this.audioContext);
      if(this.firstPlay){
        this.playSound();
        this.soundPaused = false;
        this.firstPlay = false;
      }else if(this.audioContext.state === 'running' && !this.soundPaused) {
        this.audioContext.suspend().then(() => {
          this.soundPaused = true;
        });
      } else if(this.audioContext.state === 'suspended' && this.soundPaused) {
        this.audioContext.resume().then(() => {
          this.soundPaused = false;
        });
      } 
    },
    copyBuff(){
      let dst = new ArrayBuffer(this.recordAsArrayBuffer.byteLength);
      new Uint8Array(dst).set(new Uint8Array(this.recordAsArrayBuffer));
      return dst;
    }
  }
};
</script>
<style lang="css" scoped>
.drop-zone {
  display: inline-flex;
  min-width: 100%;
  margin: 10px 0;
}

.drop-in{
    box-shadow: 2px 1px 15px #0095ff;
    box-shadow: 2px 1px 15px #0095ff;
}

.instru {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100px;
  height: 70px;
  margin-right: 10px;
  background-color: #444444;
  border-radius: 4px;
  box-shadow: 2px 2px 3px #222222;
}

.A{
    border: 2px solid #d66f6f;
}

.B{
    border: 2px solid #6fd674;
}

.C{
    border: 2px solid #6f87d6;
}

.D{
    border: 2px solid #d4d66f;
}

.instru img {
  width: 60px;
  height: 60px;
}

.track {
  min-width: 90%;
  height: 70px;
  background-color: #444444;
  border-radius: 4px;
  box-shadow: 2px 2px 3px #222222;
}
</style>