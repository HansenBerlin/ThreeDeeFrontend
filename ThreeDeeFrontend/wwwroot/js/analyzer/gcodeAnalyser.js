var gcodeProcessorWorker = new Worker('js/analyzer/gcodeProcessor.js');
var doit;
var gcodeLines = undefined;
var selectedSettings = 0;
var results = Array(4);
var resultFieldIds = [];
var currentCalculationSetting = 0;

 
gcodeProcessorWorker.onmessage = function (e) {
    if ("resultFormat" in e.data) {
        addResultTableEntries(e.data.resultFormat);
    } else if ("result" in e.data) {
        results[currentCalculationSetting] = e.data.result;
        $("#layerNumber").text("(Generating...)");
        displayResult();
        initCanvas();
    } else if ("layers" in e.data) {
        gcodeProcessorWorker.postMessage("cleanup");
        initRender(e.data.layers, settingSets[currentCalculationSetting].filamentDiameter.value[0]);
    }
}

function displayResult() {
    for (var i = 0; i < resultFieldIds.length; i++) {
        var key = resultFieldIds[i];
        if (results[selectedSettings] != undefined && results[selectedSettings][key] != undefined) {
            document.getElementById(key).innerHTML = results[selectedSettings][key];
            //console.log(results[selectedSettings][key]);
        } else {
            document.getElementById(key).innerHTML = "";
        }
    }
}


var slider  = undefined;
async function onloadInit(file) {
    slider = document.getElementById("myRange");
    slider.min = 1;
    slider.max = 2;
    slider.step = 1;
    window.onresize = function () {
        clearTimeout(doit);
        doit = setTimeout(resizeCanvas, 100);
    };
    gcodeProcessorWorker.postMessage("getResultFormat");
    
    slider.oninput = function() {
        setRender(this.value)
    }
    loadSettings();
    initCanvas();
    await parseGcode(file);
}

const parseGcode = async file => {
    const response = await fetch(file)
    const text = await response.text()
    gcodeLines = text.split(/\s*[\r\n]+\s*/g);
    if (gcodeLines != undefined) {
        gcodeProcessorWorker.postMessage([gcodeLines, simpleSettingsDict(selectedSettings)]);
        currentCalculationSetting = selectedSettings;
    }
    console.log(text)
}

function addResultTableEntries(resultFormat) {
    let key;
    let cell;
    var gcodeStatsCount = 0;
    for (key in resultFormat) {
        if (resultFormat[key].table == "gcodeStats") {
            gcodeStatsCount++;
        }
    }
    var maxRow = Math.ceil(gcodeStatsCount / 1);
    var gcodeStatsTableContent = [Array(maxRow), Array(maxRow)];
    let row = 0;
    var col = 0;
    for (key in resultFormat) {
        if (resultFormat[key].table == "gcodeStats") {
            if (!key.startsWith("Category")) {
                resultFieldIds.push(key);
            }
            gcodeStatsTableContent[col][row] = key;
        }
        row++;
        if (row == maxRow) {
            row = 0;
            col++;
        }
    }
    
    let cellLeft = undefined;
    let cellRight = undefined;

    for (let i = 0; i < maxRow; i++) {
        const table = document.getElementById("gcodeStats");
        row = table.insertRow(-1);
        for (let j = 0; j < 1; j++) {
            key = gcodeStatsTableContent[j][i];
            
            if (key.startsWith("Category")) {
                if (cellLeft !== undefined){
                    cellLeft.style.borderBottomLeftRadius = "4px";
                    cellRight.style.borderBottomRightRadius = "4px";
                }
                const cell0 = row.appendChild(document.createElement('th'));
                cell0.setAttribute("colspan", 2);
                cell0.style.height = "30px";
                cell0.style.backgroundColor = "transparent";
                const row1 = table.insertRow(-1);
                let cell = row1.appendChild(document.createElement('th'));
                cell.innerHTML = resultFormat[key].discription;                
                cell.setAttribute("colspan", 2);
                cell.className = "statstable";
                cell.style.textAlign = "center";
                cell.style.paddingTop = "10px";
                cell.style.paddingBottom = "10px";
                cell.style.fontSize = "16px";
                cell.style.borderTopLeftRadius = "4px";
                cell.style.borderTopRightRadius = "4px";
                cell.style.backgroundColor = "#ffc107";
            } else {
                cellLeft = row.appendChild(document.createElement('th'));
                cellLeft.innerHTML = resultFormat[key].discription;
                cellLeft.className = "statstable";
                cellLeft.style.textAlign = "left";
                cellLeft.style.paddingLeft = "10px";                
                cellRight = row.insertCell(-1);
                cellRight.className = "statstable";
                cellRight.style.textAlign = "left";
                cellRight.style.paddingLeft = "10px";
                const span = document.createElement("span");
                span.id = key;
                cellRight.appendChild(span);
            }
        }
    }    
}
