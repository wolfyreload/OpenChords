function pageScroll() {
    margin = document.getElementById('TopMargin');
    margin.style.marginTop = screen.height/2 + 'px';
    window.scrollBy(0, 1);
    scrolldelay = setTimeout('pageScroll()', (60.0/(<%BeatsPerMinute%>+0.01)) * 1000 / 4);
}
pageScroll();