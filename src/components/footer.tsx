import React from 'react';

export interface AppFooterProps {

}

const AppFooter: React.SFC<AppFooterProps> = () => {
    return (
        <React.Fragment>

            < footer className="bg-dark" >
                <div className="container">
                    <div className="space-top-2 space-bottom-1 space-bottom-lg-2">
                        <div className="row justify-content-lg-between">
                            <div className="col-sm-3">

                                <div className="mb-4">
                                    <a href="https://htmlstream.com/front/index.html" aria-label="Front">
                                        <img className="brand" src="https://htmlstream.com/front/assets/svg/logos/logo.svg" alt="Logo" />
                                    </a>
                                </div>

                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item">
                                        <a className="nav-link media" href="#">
                                            <span className="media">
                                                <span className="fas fa-location-arrow mt-1 mr-2"></span>
                                                <span className="media-body">
                                                    153 Williamson Plaza, Maggieberg
                      </span>
                                            </span>
                                        </a>
                                    </li>
                                    <li className="nav-item">
                                        <a className="nav-link media" href="tel:1-062-109-9222">
                                            <span className="media">
                                                <span className="fas fa-phone-alt mt-1 mr-2"></span>
                                                <span className="media-body">
                                                    +1 (062) 109-9222
                      </span>
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                                {/* <!-- End Nav Link --> */}
                            </div >

                            <div className="col-sm-3">
                                <h5 className="text-white">Company</h5>

                                {/* <!-- Nav Link --> */}
                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item"><a className="nav-link" href="front.html#">About</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Careers <span className="badge badge-primary ml-1">We're hiring</span></a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Blog</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Customers</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Hire us</a></li>
                                </ul>
                                {/* <!-- End Nav Link --> */}
                            </div>

                            <div className="col-sm-3">
                                <h5 className="text-white">Features</h5>

                                {/* <!-- Nav Link --> */}
                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Press</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Release notes</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Integrations</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Pricing</a></li>
                                </ul>
                                {/* <!-- End Nav Link --> */}
                            </div>

                            <div className="col-sm-3">
                                <h5 className="text-white">Documentation</h5>

                                {/* <!-- Nav Link --> */}
                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Support</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Docs</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Status</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">API Reference</a></li>
                                    <li className="nav-item"><a className="nav-link" href="front.html#">Tech Requirements</a></li>
                                </ul>
                                {/* <!-- End Nav Link --> */}
                            </div>

                        </div >
                    </div >

                    <hr className="opacity-xs my-0" />

                    <div className="space-1">
                        <div className="row align-items-md-center mb-7">
                            <div className="col-md-6 mb-4 mb-md-0">
                                {/* <!-- Nav Link --> */}
                                <ul className="nav nav-sm nav-white nav-x-sm align-items-center">
                                    <li className="nav-item">
                                        <a className="nav-link" href="front.html#">Privacy &amp; Policy</a>
                                    </li>
                                    <li className="nav-item opacity mx-3">&#47;</li>
                                    <li className="nav-item">
                                        <a className="nav-link" href="front.html#">Terms</a>
                                    </li>
                                    <li className="nav-item opacity mx-3">&#47;</li>
                                    <li className="nav-item">
                                        <a className="nav-link" href="front.html#">Site Map</a>
                                    </li>
                                </ul>
                                {/* <!-- End Nav Link --> */}
                            </div>

                            <div className="col-md-6 text-md-right">
                                <ul className="list-inline mb-0">
                                    {/* <!-- Social Networks --> */}
                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon btn-soft-light" href="front.html#">
                                            <i className="fab fa-facebook-f"></i>
                                        </a>
                                    </li>
                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon btn-soft-light" href="front.html#">
                                            <i className="fab fa-google"></i>
                                        </a>
                                    </li>
                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon btn-soft-light" href="front.html#">
                                            <i className="fab fa-twitter"></i>
                                        </a>
                                    </li>
                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon btn-soft-light" href="front.html#">
                                            <i className="fab fa-github"></i>
                                        </a>
                                    </li>
                                    {/* <!-- End Social Networks -->

              <!-- Language --> */}
                                    <li className="list-inline-item">
                                        <div className="hs-unfold">
                                            <a className="js-hs-unfold-invoker dropdown-toggle btn btn-xs btn-soft-light" href="#"
                                                data-hs-unfold-options='{
                        "target": "#footerLanguage",
                        "type": "css-animation",
                        "animationIn": "slideInDown"
                       }'>
                                                <img className="dropdown-item-icon" src="front/assets/vendor/flag-icon-css/flags/4x3/us.svg" alt="United States Flag" />
                                                <span>United States</span>
                                            </a>

                                            <div id="footerLanguage" className="hs-unfold-content dropdown-menu dropdown-unfold dropdown-menu-bottom mb-2">
                                                <a className="dropdown-item active" href="front.html#">English</a>
                                                <a className="dropdown-item" href="front.html#">Deutsch</a>
                                                <a className="dropdown-item" href="front.html#">Español</a>
                                                <a className="dropdown-item" href="front.html#">Français</a>
                                                <a className="dropdown-item" href="front.html#">Italiano</a>
                                                <a className="dropdown-item" href="front.html#">日本語</a>
                                                <a className="dropdown-item" href="front.html#">한국어</a>
                                                <a className="dropdown-item" href="front.html#">Nederlands</a>
                                                <a className="dropdown-item" href="front.html#">Português</a>
                                                <a className="dropdown-item" href="front.html#">Русский</a>
                                            </div>
                                        </div>
                                    </li>
                                    {/* <!-- End Language --> */}
                                </ul>
                            </div>
                        </div>

                        {/* <!-- Copyright --> */}
                        <div className="w-md-75 text-lg-center mx-lg-auto">
                            <p className="text-white opacity-sm small">&copy; Front. 2020 Htmlstream. All rights reserved.</p>
                            <p className="text-white opacity-sm small">When you visit or interact with our sites, services or tools, we or our authorised service providers may use cookies for storing information to help provide you with a better, faster and safer experience and for marketing purposes.</p>
                        </div>
                        {/* <!-- End Copyright --> */}
                    </div>
                </div >
            </footer >
            {/* <!-- ========== END FOOTER ========== --> */}

        </React.Fragment>
    );
}


export default AppFooter;