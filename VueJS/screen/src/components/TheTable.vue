<template>
    <div class="main-container">
        <div class="table-container">
            <input type="radio" name="zone" id="a" value="A" @click="showInstruments($event)">
            <label for="a" class="zone a"></label>
            <input type="radio" name="zone" id="b" value="B" @click="showInstruments($event)">
            <label for="b" class="zone b"></label>
            <input type="radio" name="zone" id="c" value="C" @click="showInstruments($event)">
            <label for="c" class="zone c"></label>
            <input type="radio" name="zone" id="d" value="D" @click="showInstruments($event)">
            <label for="d" class="zone d"></label>
        </div>
        <div v-if="selectedZone === 'A'" class="instru-list">
            <drag v-for="(instru, index) in instruA" :key="index" class="instru" :data="{zone: 'A', instru: instru}">
                <img :src="require(`@/assets/icons/${instru.instru}.png`)" :alt="instru.instru">
            </drag>
        </div>
        <div v-if="selectedZone === 'B'" class="instru-list">
            <drag v-for="(instru, index) in instruB" :key="index" class="instru" :data="{zone: 'B', instru: instru}">
                <img :src="require(`@/assets/icons/${instru.instru}.png`)" :alt="instru.instru">
            </drag>
        </div>
        <div v-if="selectedZone === 'C'" class="instru-list">
            <drag v-for="(instru, index) in instruC" :key="index" class="instru" :data="{zone: 'C', instru: instru}">
                <img :src="require(`@/assets/icons/${instru.instru}.png`)" :alt="instru.instru">
            </drag>
        </div>
        <div v-if="selectedZone === 'D'" class="instru-list">
            <drag v-for="(instru, index) in instruD" :key="index" class="instru" :data="{zone: 'D', instru: instru}">
                <img :src="require(`@/assets/icons/${instru.instru}.png`)" :alt="instru.instru">
            </drag>
        </div>
    </div>
</template>
<script>
import { Drag } from 'vue-easy-dnd';

export default {
    components:{
        Drag
    },
    data(){
        return{
            selectedZone: '',
            instruA:[
                {instru: 'piano'}
            ],
            instruB:[
                {instru: 'piano'},
                {instru: 'guitar'},
                {instru: 'drum'},
                {instru: 'piano'},
                {instru: 'guitar'},
                {instru: 'drum'}
            ],
            instruC:[
                {instru: 'piano'}
            ],
            instruD:[
                {instru: 'piano'}
            ],
        }
    },
    methods: {
        showInstruments(event){
            if(this.selectedZone != event.target.value){
                if(event.target.checked){
                    this.selectedZone = event.target.value; 
                }
            }else{
                this.selectedZone = '';
            }
            console.log(this.selectedZone);
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
        min-width: 45%;
        height: 33%;
        margin-top: 5px;
        position: relative;
    }

    .zone::before{
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%,-50%);
        font-size: 2em;
        font-family: sans-serif;
        font-weight: bold;
        opacity: 0.1;
    }

    .a{
        border-color: red;
        background-color: #d66f6f;
    }

    .a::before{
        content: "A";
        /* color: red; */
    }

    .b{
        border-color: #00d30b;
        background-color: #6fd674;
    }

    .b::before{
        content: "B";
        /* color: green; */
    }

    .c{
        border-color: #0031d3;
        background-color: #6f87d6;
    }

    .c::before{
        content: "C";
        /* color: #0031d3; */
    }

    .d{
        border-color: yellow;
        background-color: #d4d66f;
    }

    .d::before{
        content: "D";
        /* color: yellow; */
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

</style>