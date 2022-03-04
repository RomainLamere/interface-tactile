<template>
    <div class="main-container">
      <Alert :zone="lastZoneReceived"></Alert>
        <div class="table-container">
            <input type="radio" name="zone" id="a" ref="rA" value="A" @click="showInstruments($event)"/>
            <label for="a" class="zone a">
                <span v-if="unseen.A" class="new-sound">!</span>
                <span v-else>{{recordsA.length}}</span>
            </label>
            <input type="radio" name="zone" id="b" ref="rB" value="B" @click="showInstruments($event)"/>
            <label for="b" class="zone b">
                <span v-if="unseen.B" class="new-sound">!</span>
                <span v-else>{{recordsB.length}}</span>
            </label>
            <input type="radio" name="zone" id="c" ref="rC" value="C" @click="showInstruments($event)"/>
            <label for="c" class="zone c">
                <span v-if="unseen.C" class="new-sound">!</span>
                <span v-else>{{recordsC.length}}</span>            </label>
            <input type="radio" name="zone" id="d" ref="rD" value="D" @click="showInstruments($event)"/>
            <label for="d" class="zone d">
                <span v-if="unseen.D" class="new-sound">!</span>
                <span v-else>{{recordsD.length}}</span>            </label>
        </div>
        <div v-if="selectedZone === 'A'" class="instru-list">
            <drag v-for="(record, index) in recordsA" :key="index" class="instru" :data="record">
                <img :src="require(`@/assets/icons/${record.instrument}.png`)" :alt="record.instrument" draggable="false">
            </drag>
        </div>
        <div v-if="selectedZone === 'B'" class="instru-list">
            <drag v-for="(record, index) in recordsB" :key="index" class="instru" :data="record">
                <img :src="require(`@/assets/icons/${record.instrument}.png`)" :alt="record.instrument" draggable="false">
            </drag>
        </div>
        <div v-if="selectedZone === 'C'" class="instru-list">
            <drag v-for="(record, index) in recordsC" :key="index" class="instru" :data="record">
                <img :src="require(`@/assets/icons/${record.instrument}.png`)" :alt="record.instrument" draggable="false">
            </drag>
        </div>
        <div v-if="selectedZone === 'D'" class="instru-list">
            <drag v-for="(record, index) in recordsD" :key="index" class="instru" :data="record">
                <img :src="require(`@/assets/icons/${record.instrument}.png`)" :alt="record.instrument" draggable="false">
            </drag>
        </div>
    </div>
</template>
<script>
import { Drag } from 'vue-easy-dnd';
import Alert from "./Alert";

export default {
    components:{
      Alert,
        Drag
    },
    data(){
        return{
            selectedZone: '',
            recordsA:[],
            recordsB:[],
            recordsC:[],
            recordsD:[],
            lastZoneReceived: '',
            unseen: {
                A: false,
                B: false,
                C: false,
                D: false
            }
        }
    },
    sockets:{},
    methods: {
        showInstruments(event){
            if(this.selectedZone != event.target.value){
                if(event.target.checked){
                    this.selectedZone = event.target.value;
                    this.unseen[this.selectedZone] = false;
                }
            }else{
                this.$refs[`r${this.selectedZone}`].checked = false;
                this.selectedZone = '';
            }
            console.log(this.selectedZone);
        },
      displayAlert(zone){
        this.lastZoneReceived = zone;
        setTimeout(()=>{
            this.lastZoneReceived = '';
        },5000);
      }
    },
    created(){
        this.$options.sockets.addRecord = (data) => {
            this.displayAlert(data.zone);
            if(data.zone != this.selectedZone){
                this.unseen[data.zone] = true;
            }
            switch(data.zone){
                case 'A':
                    this.recordsA.push(data);
                    break;
                case 'B':
                    this.recordsB.push(data);
                    break;
                case 'C':
                    this.recordsC.push(data);
                    break;
                case 'D':
                    this.recordsD.push(data);
                    break;
            }
        }
    }
}
</script>
<style lang="css" scoped>

    input[type=radio]{
        display: none;
    }

    label{
        cursor: pointer;
        user-select: none;
    }

    .main-container{
        height: 30%;
        width: 100%;
    }

    .table-container{
        height: 100%;
        width: 100%;
        padding: 2px;
        display: flex;
        flex-wrap: wrap;
        align-content: flex-end;
        justify-content: space-between;
        background-color: #282828;
    }

    .zone{
        /* border: 1px solid black; */
        display: flex;
        justify-content: center;
        align-items: center;
        min-width: 45%;
        height: 33%;
        margin-top: 5px;
        position: relative;
        text-align: center;
        font-size: 2.7em;
        font-family: sans-serif;
        font-weight: bold;
        color: white;
    }

    .new-sound{
        animation: newSound 2s ease infinite;
    }

    @keyframes newSound {
        0%{
            transform: scale(1);
        }
        50%{
            transform: scale(1.3);
        }
        60%{
            transform: rotate(10deg);
        }
        70%{
            transform: rotate(-10deg);
        }
        80%{
            transform: rotate(10deg);
        }
        90%{
            transform: rotate(-10deg);
        }
        100%{
            transform: rotate(10deg);
        }
    }

    .zone::before{
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%,-50%);
        font-size: 2em;
        font-family: sans-serif;
        font-weight: bold;
    }

    .a{
        border-color: red;
        background-color: #d66f6f;
    }

    .b{
        border-color: #00d30b;
        background-color: #6fd674;
    }

    .c{
        border-color: #0031d3;
        background-color: #6f87d6;
    }

    .d{
        border-color: yellow;
        background-color: #d4d66f;
    }

    input[type=radio]:checked + label{
        border: 6px solid #ffffff69;
    }

    .instru-list{
        display: flex;
        flex-direction: column;
        padding-top: 1em;
    }

    .instru{
        width: 120px;
        height: 120px;
        border-radius: 70px;
        margin: 10px auto;
        background-color: #282828;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .instru img{
        width: 70px;
        height: 70px;
    }

    #alert{
      visibility: hidden;
    }
</style>
