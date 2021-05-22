---
layout: splash
permalink: /
hidden: true
header:
  overlay_color: "#5c2d91"
  overlay_image: /assets/images/banner.png
  actions:
    - label: "<i class='fas fa-download'></i> Install now"
      url: "/getting-started/"
    - label: "<i class='fab fa-fw fa-github'></i> View on GitHub"
      url: "https://github.com/asiffermann/proffer"
excerpt: >
  .NET Core basic abstractions to configuration-driven providers<br />
  <small><a href="https://github.com/asiffermann/proffer/releases/tag/0.1.0">Latest release v0.1.0</a></small>
feature_row:
  - image_path: /assets/images/banner.png
    alt: "core"
    title: "Core libraries"
    excerpt: "Provide common abstractions for any Proffer project or provider."
    url: "/core/"
    btn_class: "btn--primary"
    btn_label: "Learn more"
  - image_path: /assets/images/banner.png
    alt: "storage"
    title: "Storage"
    excerpt: "File Storage abstractions with providers."
    url: "/storage/"
    btn_class: "btn--primary"
    btn_label: "Learn more"
  - image_path: /assets/images/banner.png
    alt: "templating"
    title: "Templating"
    excerpt: "Templating abstractions with providers."
    url: "/templating/"
    btn_class: "btn--primary"
    btn_label: "Learn more"    
  - image_path: /assets/images/banner.png
    alt: "email"
    title: "Email"
    excerpt: "Templated email sending abstractions with providers."
    url: "/email/"
    btn_class: "btn--primary"
    btn_label: "Learn more"    
---

{% include feature_row %}