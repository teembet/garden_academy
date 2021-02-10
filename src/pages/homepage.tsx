
import * as React from 'react';
import AppHeader from '../components/header'
import AppFooter from '../components/footer'



export interface HomePageProps {

}

const HomePage: React.SFC<HomePageProps> = () => {
    return (
        <React.Fragment>

            <AppHeader></AppHeader>

            {/* <!-- ========== MAIN CONTENT ========== --> */}
            <main id="content" role="main">
                {/* <!-- Hero Section --> */}
                <div className="d-lg-flex position-relative">
                    <div className="container d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
                        {/* <!-- Content --> */}
                        <div className="w-md-100">
                            <div className="row">
                                <div className="col-lg-5">
                                    <div className="mb-5 mt-11">
                                        <h1 className="display-4 mb-3">
                                            Turn your ideas into a

                  <span className="text-primary text-highlight-warning">
                                                <span className="js-text-animation"
                                                    data-hs-typed-options='{
                            "strings": ["startup.", "future.", "success."],
                            "typeSpeed": 90,
                            "loop": true,
                            "backSpeed": 30,
                            "backDelay": 2500
                          }'></span>
                                            </span>
                                        </h1>
                                        <p className="lead">Front's feature-rich designed demo pages help you create the best possible product.</p>
                                    </div>

                                    <a className="btn btn-primary btn-wide transition-3d-hover" href="https://htmlstream.com/front/page-login-simple.html">Get Started</a>
                                    <a className="btn btn-link btn-wide" href="front.html#">Learn More <i className="fas fa-angle-right fa-sm ml-1"></i></a>
                                </div>
                            </div>
                        </div>
                        {/* <!-- End Content -->

       
          {/* <!-- End SVG Shape --> */}
                    </div>
                </div>
                {/* <!-- End Hero Section -->

      <!-- Articles Section --> */}
                <div className="container space-2 space-top-xl-3 space-bottom-lg-3">
                    {/* <!-- Title --> */}
                    <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
                        <h2>Learn to develop sites with components and design systems</h2>
                    </div>
                    {/* <!-- End Title --> */}

                    <div className="row mx-n2 mx-lg-n3">
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0" data-aos="fade-up">
                            {/* <!-- Card --> */}
                            <a className="card bg-primary text-left h-100 transition-3d-hover" href="https://htmlstream.com/front/documentation/index.html">
                                <div className="card-body">
                                    <div className="mb-5">
                                        <h3 className="text-white">Documentation</h3>
                                        <p className="text-white">Discover how to build and maintain coding systems using our documentation.</p>
                                    </div>
                                    <img className="img-fluid w-100" src="front/assets/svg/illustrations/docs-frame.svg" alt="Image Description" />
                                </div>
                                <div className="card-footer border-0 bg-transparent pt-0">
                                    <span className="font-size-1 text-white font-weight-bold">Learn more <i className="fas fa-angle-right fa-sm ml-1"></i></span>
                                </div>
                            </a>
                            {/* <!-- End Card --> */}
                        </div>

                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0" data-aos="fade-up" data-aos-delay="150">
                            {/* <!-- Card --> */}
                            <a className="card bg-dark text-left h-100 transition-3d-hover" href="https://htmlstream.com/front/snippets/index.html">
                                <div className="card-body">
                                    <div className="mb-5">
                                        <h3 className="text-white">Snippets</h3>
                                        <p className="text-white">Start browsing our snippets pages with copy-to-clipboard snippets to match Bootstrap's level of quality.</p>
                                    </div>
                                    <img className="img-fluid w-100" src="front/assets/svg/illustrations/snippets-frame.svg" alt="Image Description" />
                                </div>
                                <div className="card-footer border-0 bg-transparent pt-0">
                                    <span className="font-size-1 text-white font-weight-bold">Start building <i className="fas fa-angle-right fa-sm ml-1"></i></span>
                                </div>
                            </a>
                            {/* <!-- End Card --> */}
                        </div>

                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3" data-aos="fade-up" data-aos-delay="200">
                            {/* <!-- Card --> */}
                            <a className="js-go-to card bg-warning text-left h-100 transition-3d-hover" href="javascript:;"
                                data-hs-go-to-options='{
              "targetSelector": "#demoExamplesSection",
              "offsetTop": 0,
              "position": null,
              "animationIn": false,
              "animationOut": false
             }'>
                                <div className="card-body">
                                    <div className="mb-5">
                                        <h3 className="text-white">Layout options</h3>
                                        <p className="text-white">Apart from 70+ HTML-pages, the theme comes with 3 ready-to-use and stand-alone demo options.</p>
                                    </div>
                                    <img className="img-fluid w-100" src="front/assets/svg/illustrations/layouts-frame.svg" alt="Image Description" />
                                </div>
                                <div className="card-footer border-0 bg-transparent pt-0">
                                    <span className="font-size-1 text-white font-weight-bold">View examples <i className="fas fa-angle-right fa-sm ml-1"></i></span>
                                </div>
                            </a>
                            {/* <!-- End Card --> */}
                        </div>
                    </div>
                </div>
                {/* <!-- End Articles Section --> */}

                {/* <!-- Testimonials Section --> */}
                <div className="bg-light rounded-lg mx-3 mx-md-11">
                    <div className="container space-1 space-md-2">
                        <div className="card bg-transparent shadow-none">
                            <div className="row">
                                <div className="col-lg-3 d-none d-lg-block">
                                    <div className="dzsparallaxer auto-init height-is-based-on-content use-loading mode-scroll bg-light" data-options='{direction: "reverse"}' style={{ overflow: "visible" }}>
                                        <div data-parallaxanimation='[{property: "transform", value:" translate3d(0,{{val}}rem,0)", initial:"4", mid:"0", final:"-4"}]'>
                                            <img className="img-fluid rounded-lg shadow-lg" src="front/assets/img/400x500/img31.jpg" alt="Image Description" />

                                            {/* <!-- SVG Shapes --> */}
                                            <figure className="max-w-15rem w-100 position-absolute bottom-0 left-0 z-index-n1">
                                                <div className="mb-n7 ml-n7">
                                                    <img className="img-fluid" src="front/assets/svg/components/dots-5.svg" alt="Image Description" />
                                                </div>
                                            </figure>
                                            {/* <!-- End SVG Shapes --> */}
                                        </div>
                                    </div>
                                </div>

                                <div className="col-lg-9">
                                    {/* <!-- Card Body --> */}
                                    <div className="card-body h-100 rounded-lg p-0 p-md-4">
                                        {/* <!-- SVG Quote --> */}
                                        <figure className="mb-3">
                                            <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="36" height="36" viewBox="0 0 8 8">
                                                <path fill="#377DFF" d="M3,1.3C2,1.7,1.2,2.7,1.2,3.6c0,0.2,0,0.4,0.1,0.5c0.2-0.2,0.5-0.3,0.9-0.3c0.8,0,1.5,0.6,1.5,1.5c0,0.9-0.7,1.5-1.5,1.5
                      C1.4,6.9,1,6.6,0.7,6.1C0.4,5.6,0.3,4.9,0.3,4.5c0-1.6,0.8-2.9,2.5-3.7L3,1.3z M7.1,1.3c-1,0.4-1.8,1.4-1.8,2.3
                      c0,0.2,0,0.4,0.1,0.5c0.2-0.2,0.5-0.3,0.9-0.3c0.8,0,1.5,0.6,1.5,1.5c0,0.9-0.7,1.5-1.5,1.5c-0.7,0-1.1-0.3-1.4-0.8
                      C4.4,5.6,4.4,4.9,4.4,4.5c0-1.6,0.8-2.9,2.5-3.7L7.1,1.3z"/>
                                            </svg>
                                        </figure>
                                        {/* <!-- End SVG Quote --> */}

                                        <div className="row">
                                            <div className="col-lg-8 mb-3 mb-lg-0">
                                                <div className="pr-lg-5">
                                                    <blockquote className="h3 font-weight-normal mb-4">I'm absolutely floored by the level of care and attention to detail the team at Htmlstream have put into this theme and for one can guarantee that I will be a return customer.</blockquote>
                                                    <div className="media">
                                                        <div className="avatar avatar-xs avatar-circle d-lg-none mr-2">
                                                            <img className="avatar-img" src="front/assets/img/100x100/img19.jpg" alt="Image Description" />
                                                        </div>
                                                        <div className="media-body">
                                                            <span className="text-dark font-weight-bold">Lewis</span>
                                                            <span className="font-size-1">&mdash; happy customer</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div className="col-lg-4 column-divider-lg">
                                                <hr className="d-lg-none" />

                                                <div className="pl-lg-5">
                                                    <span className="h1 text-primary">3,500+</span>
                                                    <p className="font-size-1">Leaders use Front to build a startup, ecommerce, portfolio and many more websites.</p>
                                                    <a className="font-size-1 text-nowrap" href="front.html#">Read the case studies <i className="fas fa-angle-right fa-sm ml-1"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    {/* <!-- End Card Body --> */}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                {/* <!-- End Testimonials Section -->

    <!-- Features Section --> */}
                <div className="container space-2 space-lg-3">
                    <div className="row justify-content-lg-between">
                        <div className="col-lg-5 order-lg-2 pl-lg-0">
                            <div className="bg-img-hero h-100 min-h-450rem rounded-lg" style={{ backgroundImage: "url(front/assets/img/900x900/img19.jpg)" }}></div>
                        </div>

                        <div className="col-lg-6 order-lg-1">
                            <div className="pt-8 pb-lg-8">
                                {/* <!-- Title --> */}
                                <div className="mb-5 mb-md-7">
                                    <h2 className="mb-3">The powerful and flexible theme for all kinds of businesses</h2>
                                    <p>Whether you're creating a subscription service, an on-demand marketplace, an e-commerce store, or a portfolio showcase, Front's unmatched functionality help you create the best possible product for your users.</p>
                                </div>
                                {/* <!-- End Title --> */}

                                <div className="row">
                                    <div className="col-6 mb-3 mb-md-5">
                                        <div className="pr-lg-4">
                                            <span className="js-counter h2 text-primary">300</span>
                                            <span className="h2 text-primary">+</span>
                                            <p>Build a professional website with corporate and SaaS based components.</p>
                                        </div>
                                    </div>

                                    <div className="col-6 mb-3 mb-md-5">
                                        <div className="pr-lg-4">
                                            <span className="js-counter h2 text-primary">70</span>
                                            <span className="h2 text-primary">+</span>
                                            <p>Take advantage of more than 70 pages designed with mobile-first in mind.</p>
                                        </div>
                                    </div>

                                    <div className="col-6">
                                        <div className="pr-lg-4">
                                            <span className="js-counter h2 text-primary">95</span>
                                            <span className="h2 text-primary">%</span>
                                            <p>of our customers rated 5-star our themes over 5 years.</p>
                                        </div>
                                    </div>

                                    <div className="col-6">
                                        <div className="pr-lg-4">
                                            <span className="js-counter h2 text-primary">20</span>
                                            <span className="h2 text-primary">+</span>
                                            <p>We continually deploy improvements to Front, which handles more than 3.5k users.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                {/* <!-- End Features Section -->

    <!-- Demo Examples Section --> */}
                <div id="demoExamplesSection" className="bg-light overflow-hidden">
                    <div className="container-fluid space-2 space-lg-3 px-lg-5">
                        {/* <!-- Title --> */}
                        <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
                            <h2>Front in action</h2>
                            <p>All examples you find below are included in the download package.</p>
                        </div>
                        {/* <!-- End Title --> */}

                        <div className="row">
                            <div id="stickyBlockStartPoint" className="col-lg-3 pr-xl-5 mb-5 mb-lg-0">
                                {/* <!-- Filter --> */}
                                <div id="cbpStickyFilter" className="js-sticky-block card p-4"
                                    data-hs-sticky-block-options='{
                   "parentSelector": "#stickyBlockStartPoint",
                   "targetSelector": "#logoAndNav",
                   "breakpoint": "lg",
                   "startPoint": "#stickyBlockStartPoint",
                   "endPoint": "#stickyBlockEndPoint",
                   "stickyOffsetTop": 16
                 }'>
                                    <div id="filterControls" className="nav nav-sm nav-x-0 flex-lg-column">
                                        <div className="cbp-filter-scrollbar">
                                            <a className="cbp-filter-item cbp-filter d-flex justify-content-between align-items-center-item-active nav-link mx-2 mx-lg-0" href="javascript:;" data-filter=".landings">
                                                Landings
                    <span className="badge border badge-pill ml-2">14</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".onepages">
                                                Landing Onepages
                    <span className="badge border badge-pill ml-2">2</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".account">
                                                Account pages <span className="badge badge-success ml-2">New</span>
                                                <span className="badge border badge-pill ml-auto">9</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".portfolio">
                                                Portfolio
                    <span className="badge border badge-pill ml-2">8</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".blogs">
                                                Blogs
                    <span className="badge border badge-pill ml-2">5</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".pages">
                                                Supporting Pages
                    <span className="badge border badge-pill ml-2">19</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".authentication">
                                                Account Authentications
                    <span className="badge border badge-pill ml-2">6</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".shop">
                                                Shop
                    <span className="badge border badge-pill ml-2">10</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".course">
                                                Course
                    <span className="badge border badge-pill ml-2">4</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".app-marketplace">
                                                App Marketplace
                    <span className="badge border badge-pill ml-2">4</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex justify-content-between align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".help-desk">
                                                Help Desk
                    <span className="badge border badge-pill ml-2">3</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".real-estate">
                                                Real Estate <span className="badge badge-success ml-2">New</span>
                                                <span className="badge border badge-pill ml-auto">5</span>
                                            </a>
                                            <a className="cbp-filter-item nav-link d-flex align-items-center mx-2 mx-lg-0" href="javascript:;" data-filter=".jobs">
                                                Jobs <span className="badge badge-success ml-2">New</span>
                                                <span className="badge border badge-pill ml-auto">9</span>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- End Filter --> */}
                            </div>

                            <div className="col-lg-9 pl-xl-0">
                                <div className="cbp mt-n3 mx-n3"
                                    data-hs-cbp-options='{
                   "defaultFilter": ".landings",
                   "animationType": "fadeOut",
                   "caption": "zoom",
                   "gapHorizontal": 0,
                   "gapVertical": 0,
                   "mediaQueries": [
                     {"width": 1500, "cols": 3},
                     {"width": 1100, "cols": 3},
                     {"width": 800, "cols": 3},
                     {"width": 480, "cols": 2},
                     {"width": 380, "cols": 1}
                   ]
                 }'>
                                    {/* <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/index.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img1.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Agency <span className="small text-body">(Current page)</span></span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-analytics.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img3.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Analytics</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-studio.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img2.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Studio</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-marketing.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img5.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Marketing</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-advertisement.html" target="_blank">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img6.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Advertisement</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-consulting.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img7.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Consulting</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-portfolio.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img8.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Portfolio</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-software.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img9.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Software</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-classNameic-business.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img4.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Business</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-app-ui-kit.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img10.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">UI Kit</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-app-saas.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img11.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">SaaS</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-app-tool.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img14.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Tool</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item onepages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-onepage-saas.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img16.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">SaaS</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-app-payment.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img13.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Payment</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item onepages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-onepage-corporate.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img15.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Corporate</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item landings">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/landing-app-workflow.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img12.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Workflow</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-grid.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img32.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Grid</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-masonry.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img33.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Masonry</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-modern.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img34.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Modern</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-case-studies-branding.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img35.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Case Studies Branding</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-case-studies-product.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img36.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Case Studies Product</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-single-page-list.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img37.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Single Page List</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-single-page-grid.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img38.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Single Page Grid</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item portfolio">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/portfolio-single-page-masonry.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img39.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Single Page Masonry</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item blogs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/blog-journal.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img40.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Journal</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item blogs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/blog-metro.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img41.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Metro</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item blogs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/blog-newsroom.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img42.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Newsroom</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item blogs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/blog-profile.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img43.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Blog profile</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item blogs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/blog-single-article.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img44.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Single article</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-about-agency.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img80.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">About Agency</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-services-agency.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img81.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Services Agency</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-customers.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img82.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Customers</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-customer-story.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img83.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Customer story</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-careers.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img84.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Careers</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-careers-single.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img85.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Careers single</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-hire-us.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img86.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Hire us</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-contacts-agency.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img87.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Contacts Agency</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-contacts-start-up.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img88.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Contacts Start-up</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-pricing.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img89.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Pricing</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-faq.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img90.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">FAQ</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-terms.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img91.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Terms &amp; conditions</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-privacy.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img92.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Privacy &amp; policy</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-status.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img93.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Status</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-invoice.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img94.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Invoice</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-cover-page.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img95.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Cover page</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-coming-soon.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img96.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Coming soon</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-maintenance-mode.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img97.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Maintenance mode</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item pages">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-error-404.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img98.jpg" width="373" height="185" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Error 404</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item authentication">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-login.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img45.jpg" width="373" height="185" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Login</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item authentication">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-signup.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img46.jpg" width="373" height="185" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Signup</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item authentication">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-recover-account.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img47.jpg" width="373" height="185" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Recover account</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item authentication">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-login-simple.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img48.jpg" width="373" height="185" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Login</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item authentication">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-signup-simple.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img49.jpg" width="373" height="185" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Signup</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item authentication">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/page-recover-account-simple.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img50.jpg" width="373" height="185" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Recover account</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-classNameic.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img51.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">classNameic</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-categories.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img52.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Categories</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-categories-sidebar.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img53.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Categories sidebar</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-products-grid.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img54.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Products grid</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-products-list.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img41.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Products list</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-single-product.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img56.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Single product</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-empty-cart.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img57.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Empty cart</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-cart.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img58.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Cart</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-checkout.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img59.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Checkout</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item shop">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/shop-order-completed.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img60.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Order completed</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item course">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-course/index.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img61.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Main page</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item course">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-course/listing.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img62.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Courses</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item course">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-course/description.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img63.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Course description</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item course">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-course/author.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img64.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Author</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item app-marketplace">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-app-marketplace/index.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img65.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Apps</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item app-marketplace">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-app-marketplace/app-description.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img66.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">App description</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item app-marketplace">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-app-marketplace/app-description.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img67.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Search results</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item app-marketplace">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-app-marketplace/submit-app.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img22.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Submit app</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item help-desk">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-help-desk/index.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img68.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Help page</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item help-desk">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-help-desk/listing.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img69.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Listing</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item help-desk">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-help-desk/article-description.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img70.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Article description</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-overview.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img71.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Personal info</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-login-and-security.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img72.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Login &amp; security</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-notifications.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img73.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Notifications</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-preferences.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img74.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Preferences</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-orders.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img75.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Orders</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-wishlist.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img76.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Wishlist</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-billing.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img77.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Plans &amp; payment</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-address.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img78.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Address</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item account">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/account-teams.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img79.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Teams</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item real-estate">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-real-estate/index.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img17.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Main page</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item real-estate">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-real-estate/property-list.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img18.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Listing</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item real-estate">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-real-estate/property-grid.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img19.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Listing (Grid)</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item real-estate">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-real-estate/property-description.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img20.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Property description</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item real-estate">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-real-estate/property-seller.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img21.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Seller</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/index.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img23.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Main page</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/job-list.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img24.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Listing</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/job-grid.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img25.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Listing (Grid)</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/job-overview.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img26.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Job Overview</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/apply-for-job.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img27.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Apply for Job</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/employee.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img28.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Employee (Applicant)</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/employer.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img29.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Employer (Company)</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/upload-resume.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img30.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Upload Resume</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item -->

              <!-- Item --> */}
                                    <div className="cbp-item jobs">
                                        <a className="cbp-caption" href="https://htmlstream.com/front/demo-jobs/post-job.html">
                                            <div className="bg-white shadow-sm rounded-lg overflow-hidden p-1 m-3">
                                                <div className="cbp-caption-defaultWrap">
                                                    <img src="data:image/gif;base64,R0lGODlhAQABAPAAAP///////yH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" data-cbp-src="./assets/img/750x600/img31.jpg" width="373" height="298" alt="Image Description" />
                                                </div>
                                            </div>
                                            <div className="text-center p-3">
                                                <span className="d-block h4 mb-0">Post a Job</span>
                                            </div>
                                        </a>
                                    </div>
                                    {/* <!-- End Item --> */}
                                </div>
                            </div>
                        </div>

                        {/* <!-- Sticky Block End Point --> */}
                        <div id="stickyBlockEndPoint"></div>
                    </div>
                </div>
                {/* <!-- End Demo Examples Section --> */}

                {/* <!-- Pricing Section --> */}
                <div className="container space-top-2 space-top-lg-3">
                    {/* <!-- Title --> */}
                    <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
                        <h2>Simple, transparent pricing</h2>
                        <p>Everything you need to continuously build, connect, and ship award-winning cross-browser websites.</p>
                    </div>
                    {/* <!-- End Title --> */}

                    <div className="w-xl-80 mx-xl-auto">
                        {/* <!-- Pricing --> */}
                        <div className="card p-4 mb-3 mb-md-1" data-aos="fade-up">
                            <div className="row align-items-sm-center">
                                <div className="col">
                                    <div className="media align-items-center">
                                        <div className="min-w-8rem mr-2">
                                            <figure className="text-center">
                                                <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="32" height="32" viewBox="0 0 160 160">
                                                    <circle fill="#377DFF" opacity=".85" cx="80" cy="80" r="48" />
                                                </svg>
                                            </figure>
                                        </div>
                                        <div className="media-body">
                                            <h4 className="mb-0">Standard</h4>
                                            <small className="d-block">Single site</small>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-sm-7 col-md-5">
                                    <span className="font-size-1">Ideal for corporate, portfolio, blog, shop and many more.</span>
                                </div>
                                <div className="col-12 col-md col-lg-4 col-xl-3 text-lg-right mt-3 mt-lg-0">
                                    <a className="btn btn-block btn-outline-primary border transition-3d-hover" href="https://themes.getbootstrap.com/product/front-multipurpose-responsive-template/" target="_blank">Purchase for $49</a>
                                </div>
                            </div>
                        </div>
                        {/* <!-- End Pricing -->

        <!-- Pricing --> */}
                        <div className="card p-4 mb-3 mb-md-1" data-aos="fade-up" data-aos-delay="150">
                            <div className="row align-items-sm-center">
                                <div className="col">
                                    <div className="media align-items-center">
                                        <div className="min-w-8rem mr-2">
                                            <figure className="text-center">
                                                <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="32" height="32" viewBox="0 0 160 160">
                                                    <circle fill="#377DFF" opacity=".85" cx="48" cy="53" r="48" />
                                                    <circle fill="#377DFF" opacity=".85" cx="112" cy="53" r="48" />
                                                    <circle fill="#377DFF" opacity=".85" cx="81" cy="107" r="48" />
                                                </svg>
                                            </figure>
                                        </div>
                                        <div className="media-body">
                                            <h4 className="mb-0">Multisite</h4>
                                            <small className="d-block">Unlimited sites</small>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-sm-7 col-md-5">
                                    <span className="font-size-1">All the same examples as the Standard License, but you could build all of them with a single Multisite license.</span>
                                </div>
                                <div className="col-12 col-md col-lg-4 col-xl-3 text-lg-right mt-3 mt-lg-0">
                                    <a className="btn btn-block btn-outline-primary border transition-3d-hover" href="https://themes.getbootstrap.com/product/front-multipurpose-responsive-template/" target="_blank">Purchase for $149</a>
                                </div>
                            </div>
                        </div>
                        {/* <!-- End Pricing -->

        <!-- Pricing --> */}
                        <div className="card p-4 mb-3 mb-md-1" data-aos="fade-up" data-aos-delay="200">
                            <div className="row align-items-sm-center">
                                <div className="col">
                                    <div className="media align-items-center">
                                        <div className="min-w-8rem mr-2">
                                            <figure className="text-center">
                                                <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="56" height="56" viewBox="0 0 160 160">
                                                    <circle fill="#377DFF" opacity=".85" cx="80" cy="80" r="48" />
                                                </svg>
                                            </figure>
                                        </div>
                                        <div className="media-body">
                                            <h4 className="mb-0">Extended</h4>
                                            <small className="d-block">For paying users</small>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-sm-7 col-md-5">
                                    <span className="font-size-1">Best suited for "paid subscribers" and SaaS analytics applications.</span>
                                </div>
                                <div className="col-12 col-md col-lg-4 col-xl-3 text-lg-right mt-3 mt-lg-0">
                                    <a className="btn btn-block btn-outline-primary border transition-3d-hover" href="https://themes.getbootstrap.com/product/front-multipurpose-responsive-template/" target="_blank">Purchase for $599</a>
                                </div>
                            </div>
                        </div>
                        {/* <!-- End Pricing --> */}
                    </div>
                </div>
                {/* <!-- End Pricing Section --> */}

                {/* <!-- Tools Section --> */}
                <div className="position-relative gradient-y-gray">
                    <div className="container space-2 space-top-lg-3 space-bottom-sm-3 space-bottom-lg-4">
                        {/* <!-- Title --> */}
                        <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
                            <h2>Build tools and full documention</h2>
                            <p>Components, plugins, and build tools are all thoroughly documented with live examples and markup for easier use and customization.</p>
                        </div>
                        {/* <!-- End Title --> */}

                        <div className="w-md-80 w-lg-50 mx-md-auto mb-5 mb-md-9">
                            {/* <!-- Code Sample --> */}
                            <div className="card bg-dark mb-5">
                                <div className="card-body text-monospace font-size-1 p-6">
                                    <div className="mb-6">
                                        <span className="d-block text-white-70"> $ npm install</span>
                                        <span className="d-block h4 text-success font-weight-normal">Everything installed!</span>
                                    </div>
                                    <div className="mb-6">
                                        <span className="d-block text-white-70"> $ gulp</span>
                                        <span className="d-block h4 text-success font-weight-normal">scss watching</span>
                                        <span className="d-block h4 text-success font-weight-normal">LiveReload started</span>
                                        <span className="d-block h4 text-success font-weight-normal">Opening localhost:3000</span>
                                    </div>
                                    <div className="mb-0">
                                        <span className="d-block text-white-70"> $ that's it?!</span>
                                        <span className="d-block h4 text-success font-weight-normal">Yup, that's it.</span>
                                    </div>
                                </div>
                            </div>
                            {/* <!-- End Code Sample --> */}

                            {/* <!-- Info --> */}
                            <div className="text-center mb-7">
                                <p>Not comfortable diving that deep? No worries, you just use the compiled CSS and examples pages! <a className="font-weight-bold" href="https://htmlstream.com/front/documentation/gulp.html">Learn more <span className="fas fa-angle-right ml-1"></span></a></p>
                            </div>
                            {/* <!-- End Info --> */}

                            {/* <!-- Clients --> */}
                            <div className="row justify-content-center">
                                <div className="col-4 col-sm-3 my-2">
                                    {/* <!-- Logo --> */}
                                    <figure>
                                        <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 226.3 88">
                                            <path fill="#bdc5d1" d="M70.1,76.7c0,6.3,5.1,11.4,11.4,11.4H147c6.3,0,11.4-5.1,11.4-11.4V11.2c0-6.3-5.1-11.4-11.4-11.4H81.4
                    c-6.3,0-11.4,5.1-11.4,11.4V76.7L70.1,76.7z"/>
                                            <path fill="#fff" d="M106.7,38.9V26.4h11c1,0,2.1,0.1,3,0.3c1,0.2,1.8,0.5,2.6,0.9c0.7,0.4,1.3,1.1,1.8,1.9c0.4,0.8,0.7,1.8,0.7,3.1
                    c0,2.2-0.7,3.9-2,4.9c-1.3,1-3.1,1.5-5.2,1.5L106.7,38.9L106.7,38.9z M94.9,17.2v53.4h25.9c2.4,0,4.7-0.3,7-0.9s4.3-1.5,6.1-2.8
                    c1.8-1.2,3.2-2.9,4.2-4.8c1-2,1.6-4.3,1.6-7c0-3.3-0.8-6.2-2.4-8.6c-1.6-2.4-4.1-4-7.4-5c2.4-1.1,4.2-2.6,5.4-4.4
                    c1.2-1.8,1.8-4,1.8-6.7c0-2.5-0.4-4.6-1.2-6.3c-0.8-1.7-2-3.1-3.5-4.1c-1.5-1-3.3-1.8-5.4-2.2c-2.1-0.4-4.4-0.7-7-0.7H94.9
                    L94.9,17.2z M106.7,61.5V46.9h12.8c2.5,0,4.6,0.6,6.1,1.8c1.5,1.2,2.3,3.1,2.3,5.9c0,1.4-0.2,2.5-0.7,3.4s-1.1,1.6-1.9,2.1
                    c-0.8,0.5-1.7,0.9-2.8,1.1c-1,0.2-2.1,0.3-3.3,0.3H106.7L106.7,61.5z"/>
                                        </svg>
                                    </figure>
                                    {/* <!-- End Logo --> */}
                                </div>

                                <div className="col-4 col-sm-3 my-2">
                                    {/* <!-- Logo --> */}
                                    <figure>
                                        <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 226.3 88">
                                            <path fill="#bdc5d1" d="M155.1,50.7c-4.1,0-7.6,1-10.6,2.4c-1.1-2.2-2.2-4.1-2.4-5.5C142,46,141.7,45,142,43c0.3-2,1.4-4.7,1.4-5
                    c0-0.2-0.3-1.2-2.6-1.2c-2.3,0-4.4,0.4-4.6,1.1c-0.2,0.6-0.7,2-1,3.5c-0.4,2.1-4.7,9.7-7.1,13.7c-0.8-1.6-1.5-2.9-1.6-4
                    c-0.2-1.6-0.5-2.6-0.2-4.6c0.3-2,1.4-4.7,1.4-5c0-0.2-0.3-1.2-2.6-1.2c-2.3,0-4.4,0.4-4.6,1.1c-0.2,0.6-0.5,2.1-1,3.5
                    c-0.5,1.4-6.2,14.1-7.7,17.4c-0.8,1.7-1.4,3-1.9,3.9c0,0,0,0.1-0.1,0.2c-0.4,0.8-0.6,1.2-0.6,1.2s0,0,0,0c-0.3,0.6-0.7,1.1-0.8,1.1
                    c-0.1,0-0.4-1.5,0-3.6c0.9-4.4,2.9-11.3,2.9-11.5c0-0.1,0.4-1.3-1.3-1.9c-1.7-0.6-2.3,0.4-2.4,0.4c-0.1,0-0.3,0.4-0.3,0.4
                    s1.9-7.7-3.5-7.7c-3.4,0-8,3.7-10.3,7c-1.4,0.8-4.5,2.5-7.8,4.3c-1.3,0.7-2.6,1.4-3.8,2.1c-0.1-0.1-0.2-0.2-0.3-0.3
                    C75,50.8,63,45.8,63.5,36.5c0.2-3.4,1.4-12.3,23.1-23.2c17.9-8.8,32.2-6.4,34.7-1c3.5,7.8-7.6,22.1-26.2,24.2
                    c-7.1,0.8-10.8-1.9-11.7-3c-1-1.1-1.1-1.1-1.5-0.9c-0.6,0.3-0.2,1.3,0,1.8c0.6,1.4,2.8,4,6.7,5.3c3.4,1.1,11.7,1.7,21.7-2.2
                    c11.2-4.3,20-16.4,17.4-26.5C125.2,0.8,108.1-2.5,92,3.2C82.4,6.6,72,12,64.6,18.9c-8.9,8.3-10.3,15.5-9.7,18.5
                    c2.1,10.7,16.9,17.7,22.8,22.9c-0.3,0.2-0.6,0.3-0.8,0.4c-3,1.5-14.2,7.4-17.1,13.6c-3.2,7.1,0.5,12.1,3,12.8
                    c7.6,2.1,15.4-1.7,19.6-7.9c4.2-6.3,3.7-14.4,1.7-18.1c0,0,0-0.1-0.1-0.1c0.8-0.4,1.6-0.9,2.3-1.4c1.5-0.9,3-1.7,4.3-2.4
                    c-0.7,2-1.3,4.3-1.5,7.8c-0.3,4,1.3,9.2,3.5,11.2c1,0.9,2.1,0.9,2.8,0.9c2.5,0,3.6-2.1,4.9-4.6c1.5-3,2.9-6.5,2.9-6.5
                    s-1.7,9.5,3,9.5c1.7,0,3.4-2.2,4.2-3.3c0,0,0,0,0,0s0-0.1,0.1-0.2c0.2-0.3,0.3-0.4,0.3-0.4s0,0,0,0c0.7-1.2,2.2-3.9,4.5-8.4
                    c2.9-5.8,5.8-13,5.8-13s0.3,1.8,1.1,4.7c0.5,1.7,1.6,3.6,2.4,5.5c-0.7,1-1.1,1.5-1.1,1.5s0,0,0,0c-0.6,0.7-1.1,1.5-1.8,2.3
                    c-2.3,2.8-5.1,5.9-5.5,6.9c-0.4,1.1-0.3,1.9,0.5,2.5c0.6,0.5,1.7,0.5,2.9,0.5c2.1-0.1,3.6-0.7,4.3-1c1.1-0.4,2.4-1,3.7-1.9
                    c2.3-1.7,3.7-4.1,3.5-7.3c-0.1-1.7-0.6-3.5-1.3-5.1c0.2-0.3,0.4-0.6,0.6-0.9c3.6-5.3,6.4-11,6.4-11s0.3,1.8,1.1,4.7
                    c0.4,1.5,1.3,3.1,2.1,4.7c-3.4,2.7-5.5,5.9-6.2,8c-1.3,3.9-0.3,5.6,1.7,6c0.9,0.2,2.2-0.2,3.1-0.6c1.2-0.4,2.6-1,3.9-2
                    c2.3-1.7,4.5-4,4.3-7.2c-0.1-1.4-0.4-2.9-1-4.3c2.9-1.2,6.6-1.9,11.3-1.3c10.1,1.2,12.1,7.5,11.8,10.2c-0.4,2.7-2.5,4.1-3.2,4.6
                    c-0.7,0.4-0.9,0.6-0.9,0.9c0.1,0.5,0.4,0.4,1,0.4c0.8-0.1,5.3-2.2,5.5-7C171.6,57.4,165.7,50.6,155.1,50.7z M76.9,77
                    c-3.4,3.7-8,5-10.1,3.9c-2.2-1.3-1.3-6.7,2.8-10.5c2.5-2.4,5.7-4.6,7.9-5.9c0.5-0.3,1.2-0.7,2.1-1.3c0.1-0.1,0.2-0.1,0.2-0.1
                    c0.2-0.1,0.3-0.2,0.5-0.3C81.9,68.3,80.5,73.2,76.9,77z M101.4,60.4c-1.2,2.9-3.6,10.2-5.1,9.8c-1.3-0.3-2.1-5.9-0.3-11.3
                    c0.9-2.7,2.9-6,4-7.3c1.8-2,3.9-2.7,4.3-1.9C105,50.7,102.1,58.6,101.4,60.4z M121.6,70c-0.5,0.3-1,0.4-1.2,0.3
                    c-0.1-0.1,0.2-0.4,0.2-0.4s2.5-2.7,3.5-4c0.6-0.7,1.3-1.6,2-2.5c0,0.1,0,0.2,0,0.3C126.2,66.9,123,69.1,121.6,70z M137.2,66.5
                    c-0.4-0.3-0.3-1.1,0.9-3.8c0.5-1,1.6-2.8,3.5-4.5c0.2,0.7,0.4,1.3,0.3,2C141.9,64.3,139,65.8,137.2,66.5z"/>
                                        </svg>
                                    </figure>
                                    {/* <!-- End Logo --> */}
                                </div>

                                <div className="col-4 col-sm-3 my-2">
                                    {/* <!-- Logo --> */}
                                    <figure>
                                        <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 226.3 88">
                                            <path fill="#bdc5d1" d="M78.6,45.8c-0.1,0.2-0.3,0.6-0.6,1.4c-0.3,0.8-0.6,1.8-1,3c-0.4,1.2-0.8,2.5-1.2,4c-0.4,1.5-0.9,3-1.4,4.5
                    c-0.5,1.5-0.9,3.1-1.3,4.5c-0.4,1.5-0.8,2.8-1.2,4c-0.3,1.2-0.6,2.2-0.9,3c-0.2,0.8-0.4,1.3-0.4,1.4c-0.1,0.5-0.3,1-0.6,1.6
                    c-0.3,0.6-0.6,1.1-1,1.7c-0.4,0.5-0.8,1-1.3,1.3s-0.9,0.5-1.4,0.5c-0.8,0-1.4-0.3-1.8-0.8c-0.4-0.5-0.6-1.5-0.6-3v-0.6
                    c0-0.2,0-0.4,0-0.7c0-0.5,0.2-1.4,0.6-2.7c0.4-1.3,0.8-2.8,1.3-4.5c0.5-1.7,1-3.4,1.6-5.2c0.6-1.8,1.1-3.4,1.5-4.8
                    c-1.3,1.5-2.8,3-4.4,4.5c-1.6,1.5-3.2,2.8-4.8,4c-1.6,1.2-3.3,2.2-5,2.9c-1.7,0.7-3.3,1.1-4.8,1.1c-1.8,0-3.3-0.4-4.6-1.2
                    c-1.3-0.8-2.3-1.8-3.2-3.1s-1.5-2.7-1.9-4.3C40.2,57,40,55.4,40,53.8v-0.6c0-0.2,0-0.4,0-0.5c0.2-2.9,0.6-5.8,1.2-8.9
                    c0.7-3,1.5-6,2.6-9c1.1-3,2.3-5.9,3.8-8.8c1.4-2.9,3-5.6,4.7-8.1c1.7-2.5,3.5-4.9,5.4-7c1.9-2.1,3.8-4,5.8-5.5c2-1.5,4-2.7,6-3.6
                    c2-0.9,4.1-1.3,6-1.3c2.3,0,4.5,0.6,6.7,1.9c2.1,1.3,4.2,3.3,6,6.1c0.4,0.6,0.7,1.3,0.8,1.9s0.2,1.3,0.2,1.8c0,1.3-0.3,2.4-1,3.2
                    c-0.6,0.8-1.4,1.2-2.3,1.2c-0.8,0-1.6-0.4-2.3-1.3c-0.7-0.9-1.5-2.1-2.3-3.7c-0.8-1.4-1.7-2.5-2.7-3.1c-1-0.6-2-1-3.2-1
                    c-1.8,0-3.7,0.7-5.7,2.2s-4,3.4-6,5.8c-2,2.4-3.9,5.2-5.7,8.3c-1.8,3.1-3.4,6.3-4.8,9.5c-1.4,3.2-2.5,6.5-3.3,9.6
                    c-0.8,3.2-1.2,6-1.2,8.5c0,0.8,0.1,1.7,0.2,2.6s0.3,1.7,0.6,2.4c0.3,0.7,0.7,1.3,1.3,1.8c0.5,0.5,1.2,0.7,2.1,0.7
                    c0.9,0,2-0.3,3.2-0.9c1.2-0.6,2.4-1.5,3.7-2.5c1.3-1,2.6-2.2,3.9-3.5c1.3-1.3,2.5-2.6,3.6-3.9c1.1-1.3,2.1-2.6,3-3.8
                    c0.9-1.2,1.5-2.3,1.9-3.2l3.5-10.6c0.4-1.1,1-1.9,1.7-2.4c0.7-0.5,1.4-0.7,2.1-0.7c0.4,0,0.7,0.1,1.1,0.2c0.4,0.1,0.7,0.3,0.9,0.6
                    c0.3,0.3,0.5,0.6,0.7,1s0.2,0.9,0.2,1.4c0,1.4-0.1,2.8-0.4,4c-0.3,1.3-0.6,2.5-1,3.7c-0.4,1.2-0.8,2.4-1.3,3.6
                    C79.5,43.2,79.1,44.4,78.6,45.8L78.6,45.8z M126.5,54.6c-1.1,1.5-2.4,3-3.9,4.4c-1.5,1.4-3,2.7-4.6,3.8s-3.1,2-4.6,2.7
                    c-1.5,0.7-2.9,1-4.1,1s-2.2-0.4-3-1.2c-0.8-0.8-1.1-2.2-1.1-4.1c0-1.4,0.2-3,0.6-4.8c-0.7,1.2-1.5,2.4-2.5,3.5
                    c-1,1.2-2.1,2.3-3.4,3.3c-1.3,1-2.7,1.8-4.2,2.4c-1.6,0.6-3.3,0.9-5.1,0.9c-0.8,0-1.6-0.1-2.4-0.3c-0.8-0.2-1.4-0.6-2-1.1
                    s-1-1.2-1.4-2c-0.4-0.9-0.5-1.9-0.5-3.2c0,0,0.1-0.5,0.2-1.4c0.1-0.9,0.4-2.3,1-4.2c0.5-1.9,1.4-4.4,2.5-7.4c1.1-3,2.7-6.7,4.8-11
                    c0.5-1.1,1.1-2,1.8-2.4c0.7-0.5,1.4-0.7,2.3-0.7c0.4,0,0.7,0.1,1.1,0.2c0.4,0.1,0.7,0.3,1.1,0.5c0.3,0.2,0.6,0.5,0.8,0.9
                    c0.2,0.3,0.3,0.7,0.3,1.2c0,0.3,0,0.6-0.1,0.9c-0.1,0.5-0.4,1.2-0.8,2C98.7,39,98.2,40,97.6,41c-0.6,1.1-1.1,2.2-1.8,3.4
                    c-0.6,1.2-1.2,2.5-1.7,3.8c-0.5,1.3-1,2.7-1.4,4c-0.4,1.4-0.6,2.7-0.6,4c0,0.5,0.1,1,0.4,1.5c0.3,0.4,0.7,0.7,1.3,0.7
                    c1.6,0,3.2-0.5,4.7-1.6c1.5-1.1,2.9-2.4,4.1-4c1.3-1.6,2.4-3.3,3.4-5.2c1-1.8,1.9-3.5,2.6-5.1c0.5-1,0.9-2.1,1.3-3.3
                    s0.8-2.2,1.2-3.2c0.4-1,0.9-1.8,1.5-2.5c0.6-0.7,1.3-1,2.1-1c0.9,0,1.6,0.3,2.2,1s0.8,1.5,0.8,2.5c0,0.5-0.2,1.3-0.5,2.2
                    s-0.8,2-1.3,3.2c-0.5,1.2-1.1,2.5-1.7,3.9c-0.6,1.4-1.2,2.8-1.7,4.2c-0.5,1.4-0.9,2.8-1.3,4.2c-0.3,1.4-0.5,2.6-0.5,3.8
                    c0,1.1,0.6,1.6,1.7,1.6c0.8,0,1.8-0.3,2.9-0.8c1.2-0.5,2.4-1.3,3.7-2.3c1.3-1,2.6-2.1,3.8-3.4c1.3-1.3,2.4-2.7,3.4-4.3L126.5,54.6
                    L126.5,54.6z"/>
                                            <path fill="#bdc5d1" d="M127.6,52.3c-0.3,0.7-0.6,1.5-0.8,2.4c-0.3,0.9-0.4,1.7-0.4,2.4c0,0.4,0.1,0.8,0.2,1.1s0.4,0.4,0.9,0.4
                    c0.5,0,1.2-0.2,2-0.6c0.8-0.4,1.7-0.9,2.6-1.5c0.9-0.6,1.9-1.3,2.9-2.1c1-0.8,2-1.6,3-2.5c1-0.9,1.9-1.7,2.8-2.6
                    c0.9-0.9,1.7-1.6,2.4-2.4c0.2-0.2,0.4-0.4,0.7-0.4c0.3-0.1,0.5-0.1,0.7-0.1c0.5,0,0.9,0.2,1.3,0.6c0.3,0.4,0.5,1,0.5,1.7
                    c0,0.6-0.2,1.3-0.5,2.1c-0.4,0.8-1,1.5-1.9,2.3c-1.6,1.8-3.2,3.4-4.9,5c-1.6,1.6-3.3,3-4.9,4.2c-1.6,1.2-3.2,2.2-4.9,2.9
                    c-1.6,0.7-3.2,1.1-4.7,1.1c-1,0-1.9-0.2-2.6-0.5c-0.7-0.3-1.3-0.8-1.7-1.3c-0.4-0.6-0.7-1.2-0.9-2c-0.2-0.8-0.3-1.6-0.3-2.5
                    c0-1.5,0.2-3,0.6-4.5c0.4-1.5,0.8-3,1.3-4.2c0.9-2.3,1.8-4.7,2.7-7c0.9-2.3,1.7-4.4,2.5-6.3l11.5-29c0.5-1.2,1.1-2,1.8-2.5
                    c0.8-0.5,1.5-0.7,2.3-0.7s1.5,0.3,2.1,0.8c0.6,0.5,1,1.3,1,2.5c0,0.5-0.1,1.1-0.3,1.7c-0.2,0.6-0.5,1.2-0.8,1.9
                    c-0.6,1.4-1.4,3.1-2.3,5.2c-0.9,2-1.8,4.3-2.8,6.6s-2,4.9-3.1,7.5c-1,2.6-2.1,5.1-3.1,7.6c-1,2.5-1.9,4.8-2.8,7
                    C129,48.8,128.3,50.7,127.6,52.3L127.6,52.3z"/>
                                            <path fill="#bdc5d1" d="M181.4,44.2c0.4-0.2,0.7-0.4,1-0.6c0.3-0.2,0.6-0.2,0.9-0.2c0.6,0,1,0.2,1.3,0.7c0.3,0.5,0.4,1.1,0.4,1.8
                    c0,0.8-0.2,1.6-0.5,2.4c-0.3,0.8-0.9,1.5-1.5,2c-2.7,2.4-5.1,4.6-7.2,6.6c-2.1,2-4.1,3.7-6,5.1c-1.9,1.4-3.7,2.5-5.5,3.3
                    c-1.8,0.8-3.7,1.2-5.8,1.2c-1.9,0-3.4-0.4-4.5-1.3c-1-0.8-1.6-2-1.6-3.5v-0.3c0-0.1,0-0.2,0-0.4c0.1-0.9,0.5-1.9,1.2-3.1
                    c0.7-1.2,1.5-2.4,2.5-3.6s2-2.5,3.1-3.8c1.1-1.3,2.1-2.5,3-3.7c0.9-1.2,1.7-2.2,2.3-3.2c0.6-1,0.9-1.7,0.9-2.3
                    c0-0.4-0.1-0.6-0.4-0.9c-0.3-0.2-0.7-0.3-1.3-0.3c-1,0-2.1,0.3-3.1,0.8s-2.1,1.2-3.2,2.1c-1,0.9-2.1,1.9-3.1,3s-1.9,2.3-2.8,3.5
                    c-0.9,1.2-1.7,2.5-2.4,3.7c-0.7,1.2-1.3,2.4-1.8,3.4c-0.2,0.3-0.4,0.8-0.6,1.4c-0.3,0.6-0.6,1.3-0.9,2c-0.3,0.7-0.7,1.5-1,2.3
                    c-0.4,0.8-0.7,1.5-1,2.2c-0.3,0.7-0.6,1.3-0.8,1.9c-0.2,0.6-0.4,0.9-0.5,1.1c-0.1,0.3-0.3,0.8-0.6,1.5s-0.6,1.6-1,2.6
                    c-0.4,1-0.8,2.1-1.2,3.2c-0.4,1.1-0.8,2.2-1.2,3.1c-0.4,1-0.7,1.9-1,2.6c-0.3,0.7-0.5,1.3-0.5,1.5c-0.3,0.8-0.6,1.5-0.9,2.2
                    c-0.3,0.7-0.7,1.3-1.2,1.8c-0.4,0.5-0.9,0.9-1.5,1.3c-0.6,0.3-1.2,0.5-1.9,0.5c-0.9,0-1.7-0.2-2.3-0.6c-0.6-0.4-0.9-1.3-0.9-2.5
                    c0-0.6,0.1-1.2,0.2-1.8c0.2-0.6,0.3-1.3,0.6-1.9c0.2-0.6,0.5-1.2,0.7-1.8c0.2-0.6,0.5-1.1,0.7-1.7c1.4-3,2.8-6,4.2-9
                    c1.4-3,2.8-6,4.1-8.9c1.3-2.9,2.5-5.8,3.6-8.7c1.1-2.8,2-5.6,2.8-8.2c0.2-0.5,0.5-1.3,0.8-2.3c0.4-1,0.8-2,1.2-2.9
                    c0.5-1,1-1.8,1.5-2.5c0.6-0.7,1.2-1.1,1.8-1.1c1,0,1.7,0.2,2.2,0.7c0.5,0.5,0.7,1.1,0.7,2c0,0.2,0,0.5-0.1,0.9S153,38.6,153,39
                    c-0.1,0.4-0.2,0.8-0.2,1.2c-0.1,0.4-0.2,0.7-0.2,0.9c0.9-1,1.9-2,3-3c1.1-1,2.2-1.9,3.4-2.7c1.2-0.8,2.3-1.5,3.6-2
                    c1.2-0.5,2.4-0.8,3.6-0.8c0.8,0,1.7,0.1,2.5,0.4c0.8,0.2,1.6,0.6,2.2,1.1c0.6,0.5,1.2,1,1.6,1.8c0.4,0.7,0.6,1.5,0.6,2.4
                    c0,1.2-0.3,2.5-0.9,3.9c-0.6,1.4-1.4,2.8-2.3,4.2c-0.9,1.4-1.9,2.8-3,4.1c-1.1,1.3-2.1,2.5-3.1,3.6c-0.9,1.1-1.7,2-2.4,2.8
                    c-0.6,0.8-1,1.3-1,1.5c0,0.4,0.1,0.6,0.3,0.9c0.2,0.2,0.6,0.3,1.1,0.3c0.4,0,1.1-0.2,1.9-0.7c0.8-0.5,2-1.3,3.5-2.5s3.4-2.8,5.7-4.8
                    C175.1,49.7,178,47.2,181.4,44.2L181.4,44.2z"/>
                                        </svg>
                                    </figure>
                                    {/* <!-- End Logo --> */}
                                </div>

                                <div className="col-4 col-sm-3 my-2">
                                    {/* <!-- Logo --> */}
                                    <figure>
                                        <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 226.3 88">
                                            <path fill="#bdc5d1" d="M16.3,6.3h193.6v64.5h-96.8v10.7H70.2V70.9H16.3V6.3z M27.1,60.1h21.5V27.9h10.7v32.3h10.7V17.1H27.1V60.1z
                     M80.9,17.1v53.8h21.5V60.1h21.5v-43H80.9z M102.4,27.9h10.7v21.5h-10.7V27.9z M134.7,17.1v43h21.5V27.9h10.7v32.3h10.7V27.9h10.7
                    v32.3h10.7V17.1H134.7z"/>
                                            <polygon fill="none" points="31.8,59.2 52.1,59.2 52.1,28.8 62.3,28.8 62.3,59.2 72.5,59.2 72.5,18.5 31.8,18.5 " />
                                            <path fill="none" d="M82.7,18.5v50.8H103V59.2h20.3V18.5H82.7z M113.1,49.1H103V28.8h10.1V49.1z" />
                                            <polygon fill="none" points="133.5,18.5 133.5,59.2 153.8,59.2 153.8,28.8 164,28.8 164,59.2 174.1,59.2 174.1,28.8 184.3,28.8184.3,59.2 194.4,59.2 194.4,18.5 " />
                                        </svg>
                                    </figure>
                                    {/* <!-- End Logo --> */}
                                </div>
                            </div>
                            {/* <!-- End Clients --> */}
                        </div>

                        <div className="text-center">
                            <a className="btn btn-primary transition-3d-hover px-lg-7" href="https://themes.getbootstrap.com/product/front-multipurpose-responsive-template/" target="_blank">Get a License for $49</a>
                        </div>
                    </div>

                    {/* <!-- SVG Bottom Shape --> */}
                    <figure className="position-absolute bottom-0 right-0 left-0">
                        <svg preserveAspectRatio="none" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 1920 100.1">
                            <path fill="#fff" d="M0,0c0,0,934.4,93.4,1920,0v100.1H0L0,0z" />
                        </svg>
                    </figure>
                    {/* <!-- End SVG Bottom Shape --> */}
                </div>
                {/* <!-- End Tools Section --> */}

                {/* <!-- Stats Section --> */}
                <div className="container space-top-1 space-top-md-2 space-bottom-2 space-bottom-lg-3">
                    <div className="row justify-content-lg-center">
                        <div className="col-md-4 mb-7 mb-lg-0">
                            <div data-aos="fade-up" data-aos-delay="100">
                                {/* <!-- Stats --> */}
                                <div className="text-center px-md-3 px-lg-7">
                                    <figure className="mb-3">
                                        <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 71.7 64" width="71" height="64">
                                            <path fill="#FFC107" d="M36.8,14.6L42,25.3c0,0.2,0.2,0.2,0.3,0.3L54,27.2c0.3,0,0.5,0.5,0.3,0.8l-8.5,8.2c-0.2,0.2-0.2,0.3-0.2,0.5
                    l2,11.7c0,0.3-0.3,0.7-0.7,0.5l-10.5-5.6c-0.2,0-0.3,0-0.5,0l-10.5,5.6c-0.3,0.2-0.8-0.2-0.7-0.5l2-11.7c0-0.2,0-0.3-0.2-0.5
                    L18,28.1c-0.3-0.3-0.2-0.8,0.3-0.8L30,25.6c0.2,0,0.3-0.2,0.3-0.3l5.3-10.7C36.1,14.2,36.6,14.2,36.8,14.6z"/>
                                            <path opacity=".25" fill="#FFC107" d="M56,5.9l1.5,2.8c0,0,0,0,0.2,0l3.1,0.5c0.2,0,0.2,0.2,0,0.2l-2.3,2.3c0,0,0,0,0,0.2l0.5,3.1
                    c0,0.2-0.2,0.2-0.2,0.2L56,13.6h-0.2L53,15.1c-0.2,0-0.2,0-0.2-0.2l0.5-3.1v-0.2l-2.3-2.3V9.2l3.1-0.5c0,0,0,0,0.2,0l1.5-2.8
                    C55.8,5.7,55.8,5.7,56,5.9z"/>
                                            <path opacity=".25" fill="#FFC107" d="M12.3,0.3l1.3,2.8c0,0,0,0,0.2,0l3,0.5c0.2,0,0.2,0.2,0,0.2l-2.1,2.1c0,0,0,0,0,0.2l0.5,3
                    c0,0.2-0.2,0.2-0.2,0.2l-2.6-1.5c0,0,0,0-0.2,0L9.5,9.2c-0.2,0-0.2,0-0.2-0.2l0.5-3c0,0,0,0,0-0.2L7.5,3.7V3.6l3-0.5c0,0,0,0,0.2,0
                    l1.3-2.8C12.1,0.3,12.3,0.3,12.3,0.3z"/>
                                            <path opacity=".25" fill="#FFC107" d="M13.9,49.9l1.5,2.8c0,0,0,0,0.2,0l3.1,0.5c0.2,0,0.2,0.2,0,0.2l-2.3,2.3c0,0,0,0,0,0.2l0.5,3.1
                    c0,0.2-0.2,0.2-0.2,0.2l-2.8-1.5h-0.2L11,59.1c-0.2,0-0.2,0-0.2-0.2l0.5-3.1v-0.2L9,53.4v-0.2l3.1-0.5c0,0,0,0,0.2,0l1.3-2.8
                    C13.8,49.8,13.9,49.8,13.9,49.9z"/>
                                            <path opacity=".25" fill="#FFC107" d="M60.8,53.5l1.6,3.1c0,0,0,0,0.2,0l3.5,0.5c0.2,0,0.2,0.2,0,0.3l-2.5,2.5c0,0,0,0,0,0.2l0.7,3.5
                    c0,0.2-0.2,0.2-0.2,0.2l-3.1-1.6h-0.2l-3.1,1.6c-0.2,0-0.2,0-0.2-0.2l0.7-3.5v-0.2l-2.5-2.5c-0.2-0.2,0-0.2,0-0.3l3.5-0.5h0.2
                    l1.6-3.1C60.4,53.4,60.6,53.4,60.8,53.5z"/>
                                        </svg>
                                    </figure>
                                    <p className="mb-0"><span className="text-dark font-weight-bold">4.83 out of 5 starts</span> from 53 reviews</p>
                                </div>
                                {/* <!-- End Stats --> */}
                            </div>
                        </div>

                        <div className="col-md-4 mb-7 mb-lg-0">
                            <div data-aos="fade-up">
                                {/* <!-- Stats --> */}
                                <div className="text-center column-divider-md column-divider-20deg px-md-3 px-lg-7">

                                    <p className=" mb-0">Over <span className="text-dark font-weight-bold">500</span> support questions have been closed</p>
                                </div>
                                {/* <!-- End Stats --> */}
                            </div>
                        </div>

                        <div className="col-md-4">
                            <div data-aos="fade-up" data-aos-delay="100">
                                {/* <!-- Stats --> */}
                                <div className="text-center column-divider-md column-divider-20deg px-md-3 px-lg-7">
                                    <figure className="mb-3">
                                        <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="71" height="64" viewBox="0 0 71.7 64">
                                            <path fill="none" stroke="#21325b" strokeWidth={2} d="M47.9,1.3H20.1c-2,0-3.5,1.5-3.5,3.5v51.4c0,2,1.5,3.5,3.5,3.5h36.5c2,0,3.5-1.5,3.5-3.5v-8.6V21.2v-7.5
                    L47.9,1.3z"/>
                                            <path fill="#21325b" d="M49.1,14.7c-1.1,0-1.8-0.9-1.8-1.8V2L60,14.7H49.1z" />
                                            <line fill="none" stroke="#21325b" strokeWidth={2} strokeLinecap={"round"} x1="48.2" y1="21" x2="28" y2="21" />
                                            <line fill="none" stroke="#21325b" strokeWidth={2} strokeLinecap={"round"} x1="48.2" y1="27.9" x2="28" y2="27.9" />
                                            <line fill="none" stroke="#21325b" strokeWidth={2} strokeLinecap={"round"} x1="48.2" y1="34.8" x2="28" y2="34.8" />
                                            <line fill="none" stroke="#21325b" strokeWidth={2} strokeLinecap={"round"} x1="48.2" y1="42" x2="28" y2="42" />
                                            <path opacity=".2" fill="#21325b" d="M17.1,56V10.2c0-1.4-1.1-2.5-2.5-2.5h-0.5c-1.4,0-2.5,1.1-2.5,2.5v51.1c0,1.4,1.1,2.5,2.5,2.5h2.9h34.7
                    c1.4,0,2.5-1.1,2.5-2.5v-0.5c0-1.4-1.1-2.5-2.5-2.5H19.5C18.1,58.4,17.1,57.4,17.1,56z"/>
                                        </svg>
                                    </figure>
                                    <p className="mb-0"><span className="text-dark font-weight-bold">3,700</span> Front copies have been purchased</p>
                                </div>
                                {/* <!-- End Stats --> */}
                            </div>
                        </div>
                    </div >
                </div >
                {/* < !--End Stats Section-- > */}
            </main >
            {/*  < !-- ========== END MAIN CONTENT ========== --> */}

            <AppFooter></AppFooter>

        </React.Fragment>
    );
}

export default HomePage;