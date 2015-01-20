 function getRandomImage() {
    var num = Math.floor(Math.random() * 13)+1;
    var imgStr = '<img src="/Content/images/header-pattern-' + num + '.jpg" alt = "" class="img-responsive">';
    document.write(imgStr);
    document.close();
}
 function getRandomImageUrl() {
     var num = Math.floor(Math.random() * 13) + 1;
     return '/Content/images/header-pattern-' + num + '.jpg';
 }
