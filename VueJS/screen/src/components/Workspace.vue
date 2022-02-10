<template>
  <div class="workspace-container">
    <div>
      <input type="number" v-model="bpm">
      <button @click="sendBPM()">Send new BPM</button>
    </div>
    <div class="color-picker">
      <LightsTrack></LightsTrack>
    </div>
    <div class="sound">
      <div class="playButton">
        <img v-if="!play" id="playButton" src="@/assets/icons/bouton-jouer.png" @click="checkIfCanPlayTrack()" alt=""/>
        <img v-if="play" id="pauseButton" src="@/assets/icons/bouton-pause.png" @click="checkIfCanPlayTrack()" alt=""/>
      </div>
      <div class="tracks">
        <div class="tracksList">
          <TrackLine v-for="(n, index) in trackArray" :key="index" v-bind:index="index" v-on:addLine="disableTrack()" v-on:canPlay="playTrack($event)" v-on:lineIsStop="checkPlay($event)" v-on:lineIsStart="startButton($event)" :busCol="busCol" ></TrackLine>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import LightsTrack from './LightsTrack.vue';
import Vue from "vue";
import TrackLine from "./TrackLine";

export default {
  name: "Workspace",
  components:{
    TrackLine,
    LightsTrack
  },
  data(){
    return {
      busCol: new Vue(),
      bpm: 0,
      trackArray: [0],
      nbEventReceived : 0,
      arrayTrackEventFinishReceived : [],
      canPlay : true,
      play : false
    }
  },
  sockets:{},
  methods:{
    sendBPM(){
      console.log('Send BPM');
      this.$socket.emit('newBPM',this.bpm);
    },
    disableTrack(){
      this.trackArray.push(this.trackArray[this.trackArray.length-1]+1);
      this.arrayTrackEventFinishReceived.push(false);
      console.log('Src added in track col');
    },
    checkIfCanPlayTrack(){
      this.busCol.$emit('checkIfCanPlayTrack', {
      });
    },
    playTrack($event){
      this.nbEventReceived ++;
      if($event === false){ // si seulement une track ne peut pas etre jouer alors on ne pourra pas lancer toutes les tracks ensembles
        this.canPlay = false;
      }
      if(this.nbEventReceived === this.trackArray.length){
        if(this.canPlay === true){ // on peut jouer parceque tout les tracks sont arretées
          this.busCol.$emit('playTrack', {
          });
          this.play = true;
        }else{
          // on arrete tous le monde
          this.busCol.$emit('stopTrack', {
          });
          this.play = false;
        }
        this.nbEventReceived=0;
        this.canPlay = true;
      }
    },
    checkPlay($event){
      this.arrayTrackEventFinishReceived[$event] = true;
      let pause = true;
      for(let i = 0 ; i < this.arrayTrackEventFinishReceived.length-1 ; i++){
        if(this.arrayTrackEventFinishReceived[i] === false){
          pause = false;
        }
      }
      if(pause) {
        this.play = false;
      }
    },
    startButton($event){
      this.arrayTrackEventFinishReceived[$event] = false;
      let pause = true;
      for(let i = 0 ; i < this.arrayTrackEventFinishReceived.length-1 ; i++){
        if(this.arrayTrackEventFinishReceived[i] === false){
          pause = false;
        }
      }
      if(pause) {
        this.play = false;
      }
    }
  },

};
</script>

<style lang="css" scoped>
  .workspace-container{
    width: 100%;
    overflow: hidden;
  }

  .tracks{
    min-height: 70%;
    max-height: 95%;
    background-color: #535353;
    padding: 1.5em 1em;
    overflow: scroll;
    display: flex;
    width: 100%;
  }

  .tracks::-webkit-scrollbar{
    height: 10px;
    width: 10px;
  }

  .tracks::-webkit-scrollbar-thumb{
    background-color: #ffffff13;
    border-radius: 10px;
    height: 8px;
  }

  #playButton{
    height: 75px;
    margin: 1em;
  }

  #playButton:hover{
    cursor: pointer;
  }

  #pauseButton{
    height: 75px;
    margin: 1em;
  }

  #pauseButton:hover{
    cursor: pointer;
  }

  .playButton{
    flex: 0 1 auto;
    display: flex;
    flex-direction: column;
    justify-content: center;
    height: 100%;
  }

  .tracksList{
    flex: 1 1 auto;
  }

  .sound{
    display: flex;
    height: 100%;
  }
</style>
