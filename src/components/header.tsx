import React from 'react';
import './header.css';
import gardenlogo from "../assets/img/garden-logo.svg"
export interface AppHeaderProps {

}

const AppHeader: React.SFC<AppHeaderProps> = () => {
    return (
        <React.Fragment>

            <header id="header" className="header header-box-shadow-on-scroll header-abs-top header-bg-transparent header-show-hide">

                <div className="header-section">


                    <div id="logoAndNav" className="container " >
                        {/* <!-- Nav --> */}
                        <nav className="js-mega-menu fixed-top navbar navbar-expand-lg header-color">
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
                                            <a id="homeMegaMenu" className="hs-mega-menu-invoker nav-link active" href=".">About Us</a>

                                        </li>

                                        <li className="hs-has-sub-menu navbar-nav-item">
                                            <a id="pagesMegaMenu" className="hs-mega-menu-invoker nav-link " href="." >Programs</a>

                                           
                                        </li>
                                        {/* <!-- End Pages -->

                <!-- Blog --> */}
                                        <li className="hs-has-sub-menu navbar-nav-item">
                                            <a id="blogMegaMenu" className="hs-mega-menu-invoker nav-link  " href=".">For Employers</a>

                                           
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
                                            <a id="shopMegaMenu" className="hs-mega-menu-invoker nav-link  " href="." >Blog</a>

                                           
                                        </li>
                                

                                       
                                        <li className="hs-has-mega-menu navbar-nav-item"
                                            data-hs-mega-menu-item-options='{
                      "desktop": {
                        "position": "right",
                        "maxWidth": "900px"
                      }
                    }'>
                                            <a id="demosMegaMenu" className="hs-mega-menu-invoker nav-link " href="." >Contact Us</a>

                                           
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