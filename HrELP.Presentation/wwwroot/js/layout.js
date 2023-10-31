const sideMenu = document.querySelector('aside');
const menuBtn = document.getElementById('menu-btn');
const closeBtn = document.getElementById('close-btn');
const darkMode = document.querySelector('.dark-mode');
const logoImg = document.getElementById('logo-img');

document.addEventListener("DOMContentLoaded", function () {
    const profilePhoto = document.getElementById("profilePhoto");
    const profileDropdown = document.getElementById("profileDropdown");

    // Profil fotoğrafına tıklama olayını dinle
    profilePhoto.addEventListener("click", function (event) {
        // Dropdown menü görünürse gizle, aksi takdirde görünür yap
        if (profileDropdown.style.display === "block") {
            profileDropdown.style.display = "none";
        } else {
            profileDropdown.style.display = "block";
        }

        // Tıklamayı sayfada dışında tıklamayı kapat
        event.stopPropagation();
    });

    // Dropdown menü dışında bir yere tıklanırsa dropdown menüyü kapat
    document.addEventListener("click", function (event) {
        if (event.target !== profilePhoto) {
            profileDropdown.style.display = "none";
        }
    });

    // Dropdown menü içindeki tıklamaları dinle (Opsiyonel)
    profileDropdown.addEventListener("click", function (event) {
        // Dropdown menü içindeki tıklamaların profil menüsünü kapatmamasını sağlamak için tıklamayı durdur
        event.stopPropagation();
    });
});

menuBtn.addEventListener('click', () => {
    sideMenu.style.display = 'block';
});

closeBtn.addEventListener('click', () => {
    sideMenu.style.display = 'none';
});

//darkMode.addEventListener('click', () => {
//    document.body.classList.toggle('dark-mode-variables');
//    darkMode.querySelector('span:nth-child(1)').classList.toggle('active');
//    darkMode.querySelector('span:nth-child(2)').classList.toggle('active');

//    if (document.body.classList.contains('dark-mode-variables')) {
//        // Koyu moddayız, koyu mod logosunu göster
//        logoImg.src = '/images/HrELPlogominiDark.png';
//        // Koyu mod logosunun yolunu belirtin
//    } else {
//        // Açık moddayız, açık mod logosunu göster
//        logoImg.src = '/images/HrELPlogominiLight.png';
//    }
//})

//Orders.forEach(order => {
//    const tr = document.createElement('tr');
//    const trContent = `
//        <td>${order.productName}</td>
//        <td>${order.productNumber}</td>
//        <td>${order.paymentStatus}</td>
//        <td class="${order.status === 'Declined' ? 'danger' : order.status === 'Pending' ? 'warning' : 'primary'}">${order.status}</td>
//        <td class="primary">Details</td>
//    `;
//    tr.innerHTML = trContent;
//    document.querySelector('table tbody').appendChild(tr);
//});