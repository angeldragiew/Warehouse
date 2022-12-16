function replace() {
    var priceInputs = document.getElementsByClassName('prices');

    Array.from(priceInputs).forEach(priceInput => {
        priceInput.value = priceInput.value.replace(',', '.')
    })
}

replace();

