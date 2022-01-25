<template>
    <div class="main-container">
        <div class="table-container">
            <label for="a" class="zone a"></label>
            <input type="radio" name="zone" id="a" value="A" @click="showInstruments($event)">
            <label for="b" class="zone b"></label>
            <input type="radio" name="zone" id="b" value="B" @click="showInstruments($event)">
            <label for="c" class="zone c"></label>
            <input type="radio" name="zone" id="c" value="C" @click="showInstruments($event)">
            <label for="d" class="zone d"></label>
            <input type="radio" name="zone" id="d" value="D" @click="showInstruments($event)">
        </div>
        <div>
            afficher les listes en fct de la variable selectedZone
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return{
            selectedZone: ''
        }
    },
    methods: {
        showInstruments(event){
            if(event.target.checked){
                this.selectedZone = event.target.value; 
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
        display: flex;
        flex-wrap: wrap;
        align-content: flex-end;
        justify-content: space-between;
        background-color: gray;
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

</style>