const { createServer } = require("http");
const { Server } = require("socket.io");
const fs = require("fs");

const httpServer = createServer((req, res) => {
    fs.readFile(__dirname + '/ui/client.html', (err, data) =>{
        res.writeHead(200);
        res.end(data);
    })
});

const io = new Server(httpServer, {
    cors: {
        origin: "http://localhost:8080",
        credentials: true
    },
    allowEIO3: true
});

function log(message){
    console.log(`INFO | ${message}`);
}

io.on("connection", (socket) => {
    socket.emit("toNodeUI", "Hello Node UI");

    /**
     * Event sent from the UI for this server
     * data type : {
     *      destination: string -> the name of the event to emit with the message
     *      message: string/object (to be defined) -> the data to send with the event
     * }
     */
    socket.on("fromNodeUI", (data) => {
        log(`Received data from UI: ${JSON.stringify(data)}`);
		io.emit(data.destination, data.message);
    });

    socket.on("eventFromScreen", (data) => {
        log(`Message from screen : ${data}`);
    });

    socket.on("fromUnity", (data) => {
        log(`Message from unity : ${data}`);
        io.emit('eventToScreen',data);
    });
	
	socket.on("newBPM", (data) => {
        const start = Date.now() + 5000;
		const objBPM = "{ 'bpm':'" + data + "', 'timestamp':'"+start+"'}";
		log(`Change BPM : ${objBPM}`);
		io.emit('changeBPM', objBPM)
    });


    console.log(`Client connected - ${socket.id}`);
});


httpServer.listen(3000);
console.log('Listening on port 3000');