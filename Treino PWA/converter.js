function Converte() {
    event.preventDefault();
    const value = parseInt(document.getElementById("input-temp").value);
    const fromUnitField = document.getElementById("input-unit").value;
    const toUnitField = document.getElementById("output-unit").value;
    const outputField = document.getElementById("output-temp");
    console.log(fromUnitField);
    console.log(toUnitField);
    console.log(value);
    conv = 0;
    if (fromUnitField === 'c') {
        if (toUnitField === 'f') {
            conv = value * 9 / 5 + 32;
        } else if (toUnitField === 'k') {
            conv = value + 273.15;
        }
    }
    if (fromUnitField === 'f') {
        if (toUnitField === 'c') {
            conv = (value - 32) * 5 / 9
        } else if (toUnitField === 'k') {
            conv = (value + 459.67) * 5 / 9;
        }
    }
    if (fromUnitField === 'k') {
        if (toUnitField === 'c') {
            conv = value - 273.15;
        } else if (toUnitField === 'f') {
            conv = value * 9 / 5 - 459.67;
        }
    }
  outputField.innerHTML = Math.round(conv).toString() + " " + toUnitField.toUpperCase();
}
