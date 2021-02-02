function setActiveClass(site) {
    //get elements
    var home = document.getElementById("home");
    var rules = document.getElementById("rules");
    var blog = document.getElementById("blog");

    switch (site) {
        case "Home":
            home.classList.add("active");
            break;
        case "Blog":
            blog.classList.add("active");
            break;
        case "Rules":
            rules.classList.add("active");
            break;
    }
}
