$(document).ready(function () {
    $('#employeeTabs a').on('click', function (e) {
        e.preventDefault();
        $(this).tab('show');
    });

    $('#employeeTabs a[href="#all"]').tab('show'); // Sayfa yüklendiğinde tüm çalışanlar sekmesini göster
});