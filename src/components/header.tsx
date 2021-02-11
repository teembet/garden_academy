import React from 'react';
import '../assets/css/header.css';
import gardenlogo from "../assets/img/garden-logo.svg"

export interface AppHeaderProps {

}

const AppHeader: React.SFC<AppHeaderProps> = () => {
    return (
        <React.Fragment>

            <header id="header" className="header header-color header-box-shadow-on-scroll header-abs-top header-bg-transparent header-show-hide">

                <div className="header-section header-color">


                    <div id="logoAndNav" className="container" >
                        {/* <!-- Nav --> */}
                        <nav className="js-mega-menu navbar navbar-expand-lg">
                            {/* <!-- Logo --> */}
                            <a className="navbar-brand" href="." aria-label="Front">
                                <img src={gardenlogo} alt="Logo" />
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
                                    <svg width="18" height="16" viewBox="0 0 18 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M0 0H18V2H0V0ZM0 7H18V9H0V7ZM0 14H18V16H0V14Z" fill="#041644" />
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
                                            <a id="homeMegaMenu" className="hs-mega-menu-invoker nav-link" href="#">About Us</a>

                                        </li>
                                        <li className="hs-has-mega-menu navbar-nav-item">
                                            <a id="homeMegaMenu" className="hs-mega-menu-invoker nav-link" href="#">Programs</a>

                                        </li>

                                        <li className="hs-has-mega-menu navbar-nav-item">
                                            <a id="homeMegaMenu" className="hs-mega-menu-invoker nav-link" href="#">For Employers</a>

                                        </li>

                                        <li className="hs-has-mega-menu navbar-nav-item">
                                            <a id="homeMegaMenu" className="hs-mega-menu-invoker nav-link" href="#">Blog</a>

                                        </li>

                                        <li className="hs-has-mega-menu navbar-nav-item">
                                            <a id="homeMegaMenu" className="hs-mega-menu-invoker nav-link" href="#">Contact Us</a>
                                        </li>
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