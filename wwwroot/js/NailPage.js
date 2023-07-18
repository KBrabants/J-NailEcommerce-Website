let activeIndex = 3;
let trackLocation = 0;
let trackMax = 0;
let trackMin = 0;
function cardSelector() {


    var text = document.getElementById("title");


    const groups = document.getElementsByClassName("polaroid");

    for (var i = 0; i < groups.length; i++) {
        groups[i].dataset.status = "unused";
    }


    const nextIndex = activeIndex + 1 <= groups.length ? activeIndex + 1 : 1;

    const currentGroup = document.querySelector(`[data-index="${activeIndex}"]`),
        nextGroup = document.querySelector(`[data-index="${nextIndex}"]`);

    currentGroup.dataset.status = "after";

    nextGroup.dataset.status = "active";

    activeIndex = nextIndex;
}


function moveTrack(direction) {
    const groups = document.getElementsByClassName("more-polaroid");

    trackMin = (-groups.length / 3) * 105

    trackLocation = trackLocation + (direction * -103);

    if (trackLocation > trackMax) {
        trackLocation = trackMax;
    }

    if (trackLocation < trackMin) {
        trackLocation = trackMax;
    }


    document.getElementById("track-slider").style.transform = "translateX(" + trackLocation + "%)";

}



function changeNail() {

    var text = document.getElementById('style').value;

    let visualNail = document.getElementById('visual-nail');

    if (text === "natural") {
        visualNail.style.borderRadius = "50% 50% 50% 50% / 75% 75% 25% 25%";
        visualNail.style.height = "30vw";
        visualNail.style.width = "20vw";
    }
    else if (text === 'rounded') {
        visualNail.style.borderRadius = "50% 50% 50% 50% / 43% 42% 58% 57%";
        visualNail.style.width = "20vw";
    }
    else if (text === 'square') {
        visualNail.style.borderRadius = "52% 48% 50% 50% / 0% 0% 100% 100%";
        visualNail.style.width = "20vw";
    }
    else if (text === 'roundedSquare') {
        visualNail.style.borderRadius = "50% 50% 50% 50% / 11% 11% 89% 89%";
        visualNail.style.width = "20vw";
    }
    else if (text === 'stilleto') {
        visualNail.style.borderRadius = "50% 50% 50% 50% / 75% 75% 25% 25%";
        visualNail.style.height = "30vw";
        visualNail.style.width = "10vw";
    }
}
