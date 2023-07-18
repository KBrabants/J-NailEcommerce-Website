// Intersection observer
const callback = function (entries) {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('show');
        }
    });
}
const observer = new IntersectionObserver(callback);
const targets = document.querySelectorAll('.hidden');
targets.forEach(target => {
    observer.observe(target);
})