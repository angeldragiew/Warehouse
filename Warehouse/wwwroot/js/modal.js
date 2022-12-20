function enableModal() {
    var modal = document.getElementById('myModal');

    var images = document.getElementsByClassName('product-img');

    Array.from(images).forEach(img => {
        img.addEventListener('click', function (e) {
            document.getElementById('modal-img').src = e.target.src;
            modal.style.display = 'block';

        })
    })

    window.addEventListener('click', function (e) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    })
}

enableModal();