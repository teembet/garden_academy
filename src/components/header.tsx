import React from 'react';
export interface AppHeaderProps {

}

const AppHeader: React.SFC<AppHeaderProps> = () => {
    return (
        <React.Fragment>

            <header id="header" className="header header-box-shadow-on-scroll header-abs-top header-bg-transparent header-show-hide">

                <div className="header-section">


                    <div id="logoAndNav" className="container">
                        {/* <!-- Nav --> */}
                        <nav className="js-mega-menu navbar navbar-expand-lg">
                            {/* <!-- Logo --> */}
                            <a className="navbar-brand" href="https://htmlstream.com/front/index.html" aria-label="Front">
                                <img src="https://htmlstream.com/front/assets/svg/logos/logo.svg" alt="Logo" />
                            </a>
                            {/* <!-- End Logo -->

          <!-- Responsive Toggle Button --> */}
                            <button type="button" className="navbar-toggler btn btn-icon btn-sm rounded-circle"
                                aria-label="Toggle navigation"
                                aria-expanded="false"
                                aria-controls="navBar"
                                data-toggle="collapse"
                                data-target="#navBar">
                                <span className="navbar-toggler-default">
                                    <svg width="14" height="14" viewBox="0 0 18 18" xmlns="http://www.w3.org/2000/svg">
                                        <path fill="currentColor" d="M17.4,6.2H0.6C0.3,6.2,0,5.9,0,5.5V4.1c0-0.4,0.3-0.7,0.6-0.7h16.9c0.3,0,0.6,0.3,0.6,0.7v1.4C18,5.9,17.7,6.2,17.4,6.2z M17.4,14.1H0.6c-0.3,0-0.6-0.3-0.6-0.7V12c0-0.4,0.3-0.7,0.6-0.7h16.9c0.3,0,0.6,0.3,0.6,0.7v1.4C18,13.7,17.7,14.1,17.4,14.1z" />
                                    </svg>
                                </span>
                                <span className="navbar-toggler-toggled">
                                    <svg width="14" height="14" viewBox="0 0 18 18" xmlns="http://www.w3.org/2000/svg">
                                        <path fill="currentColor" d="M11.5,9.5l5-5c0.2-0.2,0.2-0.6-0.1-0.9l-1-1c-0.3-0.3-0.7-0.3-0.9-0.1l-5,5l-5-5C4.3,2.3,3.9,2.4,3.6,2.6l-1,1 C2.4,3.9,2.3,4.3,2.5,4.5l5,5l-5,5c-0.2,0.2-0.2,0.6,0.1,0.9l1,1c0.3,0.3,0.7,0.3,0.9,0.1l5-5l5,5c0.2,0.2,0.6,0.2,0.9-0.1l1-1 c0.3-0.3,0.3-0.7,0.1-0.9L11.5,9.5z" />
                                    </svg>
                                </span>
                            </button>
                            {/* <!-- End Responsive Toggle Button --> */}

                            {/* <!-- Navigation --> */}
                            <div id="navBar" className="collapse navbar-collapse">
                                <div className="navbar-body header-abs-top-inner">
                                    <ul className="navbar-nav">

                                        <li className="hs-has-mega-menu navbar-nav-item">
                                            <a id="homeMegaMenu" className="hs-mega-menu-invoker nav-link active" href="javascript:;">Landings</a>

                                        </li>

                                        <li className="hs-has-sub-menu navbar-nav-item">
                                            <a id="pagesMegaMenu" className="hs-mega-menu-invoker nav-link nav-link-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-labelledby="pagesSubMenu">Pages</a>

                                            {/* <!-- Pages - Submenu --> */}
                                            <div id="pagesSubMenu" className="hs-sub-menu dropdown-menu" aria-labelledby="pagesMegaMenu" style={{ minWidth: "230px" }}>
                                                {/* <!-- Account --> */}
                                                <div className="hs-has-sub-menu">
                                                    <a id="navLinkPagesAccount" className="hs-mega-menu-invoker dropdown-item dropdown-item-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-controls="navSubmenuPagesAccount">Account <span className="badge badge-success badge-pill ml-2">New</span></a>

                                                    <div id="navSubmenuPagesAccount" className="hs-sub-menu dropdown-menu" aria-labelledby="navLinkPagesAccount" style={{ minWidth: "230px" }}>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-overview.html">Personal info</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-login-and-security.html">Login &amp; security</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-notifications.html">Notifications</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-preferences.html">Preferences</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-orders.html">Orders</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-wishlist.html">Wishlist</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-billing.html">Plans &amp; payment</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-address.html">Address</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/account-teams.html">Teams</a>
                                                    </div>
                                                </div>
                                                {/* <!-- Account --> */}

                                                {/* <!-- Company --> */}
                                                <div className="hs-has-sub-menu">
                                                    <a id="navLinkPagesCompany" className="hs-mega-menu-invoker dropdown-item dropdown-item-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-controls="navSubmenuPagesCompany">Company</a>

                                                    <div id="navSubmenuPagesCompany" className="hs-sub-menu dropdown-menu" aria-labelledby="navLinkPagesCompany" style={{ minWidth: "230px" }}>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-about-agency.html">About Agency</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-services-agency.html">Services Agency</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-customers.html">Customers</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-customer-story.html">Customer story</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-careers.html">Careers</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-careers-single.html">Careers Single</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-hire-us.html">Hire Us</a>
                                                    </div>
                                                </div>
                                                {/* <!-- Company -->

                    <!-- Portfolio --> */}
                                                <div className="hs-has-sub-menu">
                                                    <a id="navLinkPagesPortfolio" className="hs-mega-menu-invoker dropdown-item dropdown-item-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-controls="navSubmenuPagesPortfolio">Portfolio</a>

                                                    <div id="navSubmenuPagesPortfolio" className="hs-sub-menu dropdown-menu" aria-labelledby="navLinkPagesPortfolio" style={{ minWidth: "230px" }}>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-grid.html">Grid</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-masonry.html">Masonry</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-modern.html">Modern</a>
                                                        <div className="dropdown-divider"></div>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-case-studies-branding.html">Case Studies Branding</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-case-studies-product.html">Case Studies Product</a>
                                                        <div className="dropdown-divider"></div>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-single-page-list.html">Single Page List</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-single-page-grid.html">Single Page Grid</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/portfolio-single-page-masonry.html">Single Page Masonry</a>
                                                    </div>
                                                </div>
                                                {/* <!-- End Portfolio -->

                    <!-- Login --> */}
                                                <div className="hs-has-sub-menu">
                                                    <a id="navLinkPagesLogin" className="hs-mega-menu-invoker dropdown-item dropdown-item-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-controls="navSubmenuPagesLogin">Login &amp; Signup</a>

                                                    <div id="navSubmenuPagesLogin" className="hs-sub-menu dropdown-menu" aria-labelledby="navLinkPagesLogin" style={{ minWidth: "230px" }}>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-login.html">Login</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-signup.html">Signup</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-recover-account.html">Recover Account</a>
                                                        <div className="dropdown-divider"></div>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-login-simple.html">Login Simple</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-signup-simple.html">Signup Simple</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-recover-account-simple.html">Recover Account Simple</a>
                                                    </div>
                                                </div>
                                                {/* <!-- Signup -->

                    <!-- Contacts --> */}
                                                <div className="hs-has-sub-menu">
                                                    <a id="navLinkContactsServices" className="hs-mega-menu-invoker dropdown-item dropdown-item-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-controls="navSubmenuContactsServices">Contacts</a>

                                                    <div id="navSubmenuContactsServices" className="hs-sub-menu dropdown-menu" aria-labelledby="navLinkContactsServices" style={{ minWidth: "230px" }}>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-contacts-agency.html">Contacts Agency</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-contacts-start-up.html">Contacts Start-Up</a>
                                                    </div>
                                                </div>
                                                {/* <!-- Contacts -->

                    <!-- Utilities --> */}
                                                <div className="hs-has-sub-menu">
                                                    <a id="navLinkPagesUtilities" className="hs-mega-menu-invoker dropdown-item dropdown-item-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-controls="navSubmenuPagesUtilities">Utilities</a>

                                                    <div id="navSubmenuPagesUtilities" className="hs-sub-menu dropdown-menu" aria-labelledby="navLinkPagesUtilities" style={{ minWidth: "230px" }}>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-pricing.html">Pricing</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-faq.html">FAQ</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-terms.html">Terms &amp; Conditions</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-privacy.html">Privacy &amp; Policy</a>
                                                    </div>
                                                </div>
                                                {/* <!-- Utilities -->

                    <!-- Specialty --> */}
                                                <div className="hs-has-sub-menu">
                                                    <a id="navLinkPagesSpecialty" className="hs-mega-menu-invoker dropdown-item dropdown-item-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-controls="navSubmenuPagesSpecialty">Specialty</a>

                                                    <div id="navSubmenuPagesSpecialty" className="hs-sub-menu dropdown-menu" aria-labelledby="navLinkPagesSpecialty" style={{ minWidth: "230px" }}>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-cover-page.html">Cover Page</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-coming-soon.html">Coming Soon</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-maintenance-mode.html">Maintenance Mode</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-status.html">Status</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-invoice.html">Invoice</a>
                                                        <a className="dropdown-item " href="https://htmlstream.com/front/page-error-404.html">Error 404</a>
                                                    </div>
                                                </div>
                                                {/* <!-- Specialty --> */}
                                            </div>
                                            {/* <!-- End Pages - Submenu --> */}
                                        </li>
                                        {/* <!-- End Pages -->

                <!-- Blog --> */}
                                        <li className="hs-has-sub-menu navbar-nav-item">
                                            <a id="blogMegaMenu" className="hs-mega-menu-invoker nav-link nav-link-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false" aria-labelledby="blogSubMenu">Blog</a>

                                            {/* <!-- Blog - Submenu --> */}
                                            <div id="blogSubMenu" className="hs-sub-menu dropdown-menu" aria-labelledby="blogMegaMenu" style={{ minWidth: "230px" }}>
                                                <a className="dropdown-item " href="https://htmlstream.com/front/blog-journal.html">Journal</a>
                                                <a className="dropdown-item " href="https://htmlstream.com/front/blog-metro.html">Metro</a>
                                                <a className="dropdown-item " href="https://htmlstream.com/front/blog-newsroom.html">Newsroom</a>
                                                <div className="dropdown-divider"></div>
                                                <a className="dropdown-item " href="https://htmlstream.com/front/blog-profile.html">Blog Profile</a>
                                                <a className="dropdown-item " href="https://htmlstream.com/front/blog-single-article.html">Single Article</a>
                                            </div>
                                            {/* <!-- End Submenu --> */}
                                        </li>
                                        {/* <!-- End Blog -->

                <!-- Shop --> */}
                                        <li className="hs-has-mega-menu navbar-nav-item"
                                            data-hs-mega-menu-item-options='{
                      "desktop": {
                        "position": "right",
                        "maxWidth": "440px"
                      }
                    }'>
                                            <a id="shopMegaMenu" className="hs-mega-menu-invoker nav-link nav-link-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false">Shop</a>

                                            {/* <!-- Shop - Mega Menu --> */}
                                            <div className="hs-mega-menu dropdown-menu hs-position-right" aria-labelledby="shopMegaMenu">
                                                <div className="mega-menu-body">
                                                    <span className="d-block h5">Shop Elements</span>

                                                    <div className="row">
                                                        <div className="col-sm-6">
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-classNameic.html">classNameic</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-categories.html">Categories</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-categories-sidebar.html">Categories Sidebar</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-products-grid.html">Products Grid</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-products-list.html">Products List</a>
                                                        </div>

                                                        <div className="col-sm-6">
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-single-product.html">Single Product</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-empty-cart.html">Empty Cart</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-cart.html">Cart</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-checkout.html">Checkout</a>
                                                            <a className="dropdown-item " href="https://htmlstream.com/front/shop-order-completed.html">Order Completed</a>
                                                        </div>
                                                    </div>
                                                </div>

                                                {/* <!-- Mega Menu Banner --> */}
                                                <div className="navbar-product-banner">
                                                    <div className="d-flex align-items-end">
                                                        <img className="img-fluid mr-4" src="front/assets/img/mockups/img4.png" alt="Image Description" />
                                                        <div className="navbar-product-banner-content">
                                                            <div className="mb-4">
                                                                <span className="h4 d-block text-primary">Win T-shirt</span>
                                                                <p>Win one of our Front brand T-shirts.</p>
                                                            </div>
                                                            <a className="btn btn-sm btn-soft-primary transition-3d-hover" href="https://htmlstream.com/front/shop-classNameic.html">Learn More <i className="fas fa-angle-right fa-sm ml-1"></i></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                {/* <!-- End Mega Menu Banner --> */}
                                            </div>
                                            {/* <!-- End Shop - Mega Menu --> */}
                                        </li>
                                        {/* <!-- End Shop --> */}

                                        {/* <!-- Demos --> */}
                                        <li className="hs-has-mega-menu navbar-nav-item"
                                            data-hs-mega-menu-item-options='{
                      "desktop": {
                        "position": "right",
                        "maxWidth": "900px"
                      }
                    }'>
                                            <a id="demosMegaMenu" className="hs-mega-menu-invoker nav-link nav-link-toggle " href="javascript:;" aria-haspopup="true" aria-expanded="false">Demos</a>

                                            {/* <!-- Demos - Mega Menu --> */}
                                            <div className="hs-mega-menu dropdown-menu w-100" aria-labelledby="demosMegaMenu">
                                                <div className="row no-gutters">
                                                    <div className="col-lg-8">
                                                        <div className="navbar-promo-card-deck">
                                                            {/* <!-- Promo Item --> */}
                                                            <div className="navbar-promo-card navbar-promo-item">
                                                                <a className="navbar-promo-link" href="https://htmlstream.com/front/demo-course/index.html">
                                                                    <div className="media align-items-center">
                                                                        <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-67.svg" alt="SVG" />
                                                                        <div className="media-body">
                                                                            <span className="navbar-promo-title">Course</span>
                                                                            <span className="navbar-promo-text">Learn On-demand demo</span>
                                                                        </div>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            {/* <!-- End Promo Item -->

                          <!-- Promo Item --> */}
                                                            <div className="navbar-promo-card navbar-promo-item">
                                                                <a className="navbar-promo-link" href="https://htmlstream.com/front/demo-app-marketplace/index.html">
                                                                    <div className="media align-items-center">
                                                                        <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-45.svg" alt="SVG" />
                                                                        <div className="media-body">
                                                                            <span className="navbar-promo-title">App Marketplace</span>
                                                                            <span className="navbar-promo-text">Marketplace app demo</span>
                                                                        </div>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            {/* <!-- End Promo Item --> */}
                                                        </div>

                                                        <div className="navbar-promo-card-deck">
                                                            {/* <!-- Promo Item --> */}
                                                            <div className="navbar-promo-card navbar-promo-item">
                                                                <a className="navbar-promo-link" href="https://htmlstream.com/front/demo-help-desk/index.html">
                                                                    <div className="media align-items-center">
                                                                        <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-4.svg" alt="SVG" />
                                                                        <div className="media-body">
                                                                            <span className="navbar-promo-title">Help Desk</span>
                                                                            <span className="navbar-promo-text">Help desk demo</span>
                                                                        </div>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            {/* <!-- End Promo Item -->

                          <!-- Promo Item --> */}
                                                            <div className="navbar-promo-card navbar-promo-item">
                                                                <a className="navbar-promo-link" href="https://htmlstream.com/front/demo-real-estate/index.html">
                                                                    <div className="media align-items-center">
                                                                        <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-13.svg" alt="SVG" />
                                                                        <div className="media-body">
                                                                            <span className="navbar-promo-title">Real Estate <span className="badge badge-success badge-pill ml-1">New</span></span>
                                                                            <span className="navbar-promo-text">Real estate demo</span>
                                                                        </div>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            {/* <!-- End Promo Item --> */}
                                                        </div>

                                                        <div className="navbar-promo-card-deck">
                                                            {/* <!-- Promo Item --> */}
                                                            <div className="navbar-promo-card navbar-promo-item">
                                                                <a className="navbar-promo-link" href="https://htmlstream.com/front/demo-jobs/index.html">
                                                                    <div className="media align-items-center">
                                                                        <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-19.svg" alt="SVG" />
                                                                        <div className="media-body">
                                                                            <span className="navbar-promo-title">Jobs <span className="badge badge-success badge-pill ml-1">New</span></span>
                                                                            <span className="navbar-promo-text">Jobs demo</span>
                                                                        </div>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            {/* <!-- End Promo Item --> */}

                                                            {/* <!-- Promo Item --> */}
                                                            <div className="navbar-promo-card navbar-promo-item">
                                                                <a className="navbar-promo-link disabled" href="javascript:;">
                                                                    <div className="media align-items-center">
                                                                        <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-28.svg" alt="SVG" />
                                                                        <div className="media-body">
                                                                            <span className="navbar-promo-title">New demo</span>
                                                                            <span className="navbar-promo-text">Coming soon...</span>
                                                                        </div>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            {/* <!-- End Promo Item --> */}
                                                        </div>
                                                    </div>

                                                    {/* <!-- Promo --> */}
                                                    <div className="col-lg-4 navbar-promo d-none d-lg-block">
                                                        <a className="d-block navbar-promo-inner" href="front.html#">
                                                            <div className="position-relative">
                                                                <img className="img-fluid rounded mb-3" src="front/assets/img/380x227/img1.jpg" alt="Image Description" />
                                                            </div>
                                                            <span className="navbar-promo-text font-size-1">Front makes you look at things from a different perspectives.</span>
                                                        </a>
                                                    </div>
                                                    {/* <!-- End Promo --> */}
                                                </div>
                                            </div>
                                            {/* <!-- End Demos - Mega Menu --> */}
                                        </li>
                                        {/* <!-- End Demos -->

                <!-- Docs --> */}
                                        <li className="hs-has-mega-menu navbar-nav-item"
                                            data-hs-mega-menu-item-options='{
                      "desktop": {
                        "position": "right",
                        "maxWidth": "260px"
                      }
                    }'>
                                            <a id="docsMegaMenu" className="hs-mega-menu-invoker nav-link nav-link-toggle" href="javascript:;" aria-haspopup="true" aria-expanded="false">Docs</a>

                                            {/* <!-- Docs - Submenu --> */}
                                            <div className="hs-mega-menu dropdown-menu" aria-labelledby="docsMegaMenu" style={{ minWidth: "330px" }}>
                                                {/* <!-- Promo Item --> */}
                                                <div className="navbar-promo-item">
                                                    <a className="navbar-promo-link" href="https://htmlstream.com/front/documentation/index.html">
                                                        <div className="media align-items-center">
                                                            <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-2.svg" alt="SVG" />
                                                            <div className="media-body">
                                                                <span className="navbar-promo-title">
                                                                    Documentation
                              <span className="badge badge-primary badge-pill ml-1">v3.3</span>
                                                                </span>
                                                                <small className="navbar-promo-text">Development guides</small>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                                {/* <!-- End Promo Item -->

                    <!-- Promo Item --> */}
                                                <div className="navbar-promo-item">
                                                    <a className="navbar-promo-link" href="https://htmlstream.com/front/snippets/index.html">
                                                        <div className="media align-items-center">
                                                            <img className="navbar-promo-icon" src="front/assets/svg/icons/icon-1.svg" alt="SVG" />
                                                            <div className="media-body">
                                                                <span className="navbar-promo-title">Snippets</span>
                                                                <small className="navbar-promo-text">Start building</small>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                                {/* <!-- End Promo Item --> */}

                                                <div className="navbar-promo-footer">
                                                    {/* <!-- List --> */}
                                                    <div className="row no-gutters">
                                                        <div className="col-6">
                                                            <div className="navbar-promo-footer-item">
                                                                <span className="navbar-promo-footer-text">Check what's new</span>
                                                                <a className="navbar-promo-footer-text" href="https://htmlstream.com/front/documentation/changelog.html"> Changelog</a>
                                                            </div>
                                                        </div>
                                                        <div className="col-6 navbar-promo-footer-ver-divider">
                                                            <div className="navbar-promo-footer-item">
                                                                <span className="navbar-promo-footer-text">Have a question?</span>
                                                                <a className="navbar-promo-footer-text" href="http://htmlstream.com/contact-us"> Contact us</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    {/* <!-- End List --> */}
                                                </div>
                                            </div>
                                            {/* <!-- End Docs - Submenu --> */}
                                        </li>
                                        {/* <!-- End Docs --> */}

                                        {/* <!-- Button --> */}
                                        <li className="navbar-nav-last-item">
                                            <a className="btn btn-sm btn-primary transition-3d-hover" href="https://themes.getbootstrap.com/product/front-multipurpose-responsive-template/" target="_blank">Buy Now</a>
                                        </li>
                                        {/* <!-- End Button --> */}
                                    </ul>
                                </div>
                            </div>
                            {/* <!-- End Navigation --> */}
                        </nav>
                        {/* <!-- End Nav --> */}
                    </div>
                </div>
            </header>
            {/* <!-- ========== END HEADER ========== --> */}

        </React.Fragment>
    );
}


export default AppHeader;