import React from 'react';

export interface AppFooterProps {

}

const AppFooter: React.SFC<AppFooterProps> = () => {
    return (
        <React.Fragment>

            {/* < !-- ========== FOOTER ========== --> */}
            < footer className="bg-dark" >
                <div className="container">
                    <div className="space-top-2 space-bottom-1 space-bottom-lg-2">
                        <div className="row justify-content-lg-between">
                            <div className="col-md-3">
                                {/* <!-- Logo --> */}
                                <div className="mb-4">
                                    <h2 className="text-primary">Garden <br />
                                          Academy</h2>
                                </div>
                                {/* <!-- End Logo -->

            <!-- Nav Link --> */}
                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item">
                                        <a className="nav-link media" href="#">
                                            <span className="media">
                                                <span className="fas fa-location-arrow mt-1 mr-2"></span>
                                                <span className="media-body">
                                                    Vibranium Vally <br />
                                                    42, Local Airport Road, Ikeja <br />
                                                    Lagos
                      </span>
                                            </span>
                                        </a>
                                    </li>
                                    <li className="nav-item">
                                        <a className="nav-link media" href="tel:1-062-109-9222">
                                            <span className="media">
                                                <span className="fas fa-phone-alt mt-1 mr-2"></span>
                                                <span className="media-body">
                                                    +234 [0] 802 345 6789
                      </span>
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                                <br />

                                <a className="nav-link media" href="tel:1-062-109-9222">
                                    <span className="media">
                                        <span className="media-body">
                                            Follow Us
                                    </span>
                                    </span>
                                </a>

                                <ul className="list-inline mb-0">

                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon" href="front.html#">
                                            <svg width="24" height="23" viewBox="0 0 24 23" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <path d="M24 3.0344C22.4903 3.11367 22.5233 3.10721 22.3526 3.12348L23.2467 0.21417C23.2467 0.21417 20.4558 1.38394 19.7483 1.5932C17.8901 -0.309693 15.1282 -0.39377 13.1536 0.989012C11.5366 2.12165 10.6686 4.06668 10.9581 6.3401C7.81109 5.84252 5.15954 4.14283 3.0661 1.27754L2.40399 0.371475L1.90924 1.40668C1.28394 2.71478 1.07629 4.21043 1.3244 5.61804C1.42621 6.19552 1.59924 6.7463 1.83893 7.26202L1.27093 7.01125L1.20355 8.09257C1.13525 9.19099 1.45624 10.4732 2.06232 11.5222C2.23297 11.8176 2.4527 12.142 2.7312 12.4621L2.43768 12.4107L2.79584 13.6498C3.2666 15.2777 4.24438 16.5372 5.5133 17.2373C4.24603 17.8501 3.22229 18.241 1.53955 18.8717L0 19.4484L1.422 20.3342C1.96417 20.672 3.88 21.8 5.77295 22.1384C9.98071 22.8903 14.7182 22.278 17.9075 19.0052C20.5939 16.2486 21.4753 12.3275 21.2922 8.24695C21.2646 7.62921 21.4131 7.03963 21.7104 6.58711C22.3061 5.68104 23.9963 3.04045 24 3.0344ZM20.586 5.62743C20.0922 6.37869 19.8448 7.33817 19.8891 8.32873C20.0738 12.4447 19.0909 15.6367 16.9675 17.8157C14.4869 20.3611 10.4859 21.3602 5.99066 20.5568C5.17657 20.4114 4.3352 20.0857 3.63885 19.7605C5.04968 19.2074 6.13916 18.7144 7.89862 17.7656L10.3548 16.441L7.643 16.2432C6.34405 16.1485 5.26245 15.4312 4.59759 14.2667C4.95062 14.2433 5.29101 14.1782 5.62994 14.0706L8.21631 13.2498L5.60834 12.5221C4.34106 12.1685 3.61871 11.3046 3.23584 10.642C2.98444 10.2066 2.82019 9.76054 2.72168 9.34934C2.98315 9.42924 3.28802 9.4862 3.77966 9.54169L6.19354 9.81374L4.28101 8.11385C2.90295 6.88921 2.35071 5.04932 2.75555 3.28058C7.05633 8.36316 12.106 7.98116 12.6143 8.11552C12.5024 6.87731 12.4995 6.87439 12.4702 6.75652C11.8193 4.1347 13.2458 2.80345 13.8893 2.35281C15.2333 1.41169 17.3666 1.26982 18.8443 2.82034C19.1634 3.15498 19.595 3.28663 19.999 3.1723C20.3615 3.06965 20.6591 2.96096 20.9504 2.84267L20.3439 4.81587L21.1181 4.81649C20.972 5.03972 20.7967 5.30697 20.586 5.62743Z" fill="#051A52" />
                                            </svg>

                                        </a>
                                    </li>
                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon" href="front.html#">
                                            <svg width="24" height="28" viewBox="0 0 24 28" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <path d="M3.51562 27.8804H11.3438V18.16H8.53125V14.9555H11.3438V10.9498C11.3438 8.7411 12.9208 6.94419 14.8594 6.94419H18.375V10.1487H15.5625C14.787 10.1487 14.1562 10.8674 14.1562 11.751V14.9555H18.2481L17.7794 18.16H14.1562V27.8804H20.4844C22.4229 27.8804 24 26.0835 24 23.8747V4.5408C24 2.33206 22.4229 0.535156 20.4844 0.535156H3.51562C1.57709 0.535156 0 2.33206 0 4.5408V23.8747C0 26.0835 1.57709 27.8804 3.51562 27.8804ZM1.40625 4.5408C1.40625 3.2156 2.35254 2.13741 3.51562 2.13741H20.4844C21.6475 2.13741 22.5938 3.2156 22.5938 4.5408V23.8747C22.5938 25.1999 21.6475 26.2781 20.4844 26.2781H15.5625V19.7623H18.9706L19.9081 13.3532H15.5625V11.751H19.7812V5.34193H14.8594C12.1454 5.34193 9.9375 7.85756 9.9375 10.9498V13.3532H7.125V19.7623H9.9375V26.2781H3.51562C2.35254 26.2781 1.40625 25.1999 1.40625 23.8747V4.5408Z" fill="#051A52" />
                                            </svg>

                                        </a>
                                    </li>
                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon " href="front.html#">
                                            <svg width="24" height="28" viewBox="0 0 24 28" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <g clip-path="url(#clip0)">
                                                    <path d="M18.5898 27.8804C18.6928 27.8804 23.3518 27.8793 23.2498 27.8793C23.6638 27.8793 23.9998 27.4965 23.9998 27.0248C23.6498 18.0726 25.8418 8.62598 17.6618 8.62598C16.0888 8.62598 14.8358 9.23783 13.9328 10.0468C13.9328 8.38215 12.3538 9.30277 8.70882 9.05552C8.29482 9.05552 7.95882 9.43836 7.95882 9.91006C8.26082 26.0506 7.28482 27.8793 8.70882 27.8793H13.3688C14.7218 27.8793 13.8608 25.7053 14.1188 18.55C14.1188 15.5944 14.8688 15.0156 16.1648 15.0156C17.5988 15.0156 17.8398 16.1505 17.8398 18.6936C18.0968 25.7202 17.2418 27.8804 18.5898 27.8804ZM16.1648 13.3066C11.6738 13.3066 12.6188 18.9328 12.6188 26.1702H9.45882V10.7646H12.4328V12.2435C12.4328 13.0639 13.5298 13.4672 13.9118 12.6423C14.4038 11.5781 15.6818 10.3351 17.6618 10.3351C21.1888 10.3351 22.4998 12.3096 22.4998 17.6226V26.1714H19.3398C19.3398 18.0316 20.0958 13.3066 16.1648 13.3066Z" fill="#051A52" />
                                                    <path d="M1.12224 9.05666C-0.297764 9.05666 0.674236 10.8626 0.372236 27.0259C0.372236 27.4976 0.708236 27.8804 1.12224 27.8804H5.78724C7.20724 27.8804 6.23524 26.0745 6.53724 9.9112C6.53724 8.4357 4.65624 9.30619 1.12224 9.05666ZM5.03724 26.1713H1.87224V10.7657H5.03724V26.1713Z" fill="#051A52" />
                                                    <path d="M3.45227 0.535156C-1.12373 0.535156 -1.09573 8.42995 3.45227 8.42995C7.99727 8.42995 8.03327 0.535156 3.45227 0.535156ZM3.45227 6.72087C0.88427 6.72087 0.86027 2.24423 3.45227 2.24423C6.04927 2.24423 6.01627 6.72087 3.45227 6.72087Z" fill="#051A52" />
                                                </g>
                                                <defs>
                                                    <clipPath id="clip0">
                                                        <rect width="24" height="27.3452" fill="white" transform="translate(0 0.535156)" />
                                                    </clipPath>
                                                </defs>
                                            </svg>

                                        </a>
                                    </li>
                                    <li className="list-inline-item">
                                        <a className="btn btn-xs btn-icon" href="front.html#">
                                            <svg width="24" height="28" viewBox="0 0 24 28" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <g clip-path="url(#clip0)">
                                                    <path d="M3.51562 27.8804H20.4844C22.4229 27.8804 24 26.0835 24 23.8747V4.5408C24 2.33206 22.4229 0.535156 20.4844 0.535156H3.51562C1.57709 0.535156 0 2.33206 0 4.5408V23.8747C0 26.0835 1.57709 27.8804 3.51562 27.8804ZM1.40625 4.5408C1.40625 3.2156 2.35254 2.13741 3.51562 2.13741H20.4844C21.6475 2.13741 22.5938 3.2156 22.5938 4.5408V23.8747C22.5938 25.1999 21.6475 26.2781 20.4844 26.2781H3.51562C2.35254 26.2781 1.40625 25.1999 1.40625 23.8747V4.5408Z" fill="#051A52" />
                                                    <path d="M12 21.4179C15.4893 21.4179 18.3281 18.1833 18.3281 14.2077C18.3281 10.2321 15.4893 6.99756 12 6.99756C8.51074 6.99756 5.67188 10.2321 5.67188 14.2077C5.67188 18.1833 8.51074 21.4179 12 21.4179ZM12 8.59982C14.714 8.59982 16.9219 11.1154 16.9219 14.2077C16.9219 17.3 14.714 19.8156 12 19.8156C9.28601 19.8156 7.07812 17.3 7.07812 14.2077C7.07812 11.1154 9.28601 8.59982 12 8.59982Z" fill="#051A52" />
                                                    <path d="M19.0312 8.59974C20.1943 8.59974 21.1406 7.52156 21.1406 6.19636C21.1406 4.87116 20.1943 3.79297 19.0312 3.79297C17.8682 3.79297 16.9219 4.87116 16.9219 6.19636C16.9219 7.52156 17.8682 8.59974 19.0312 8.59974ZM19.0312 5.39523C19.4189 5.39523 19.7344 5.75469 19.7344 6.19636C19.7344 6.63802 19.4189 6.99749 19.0312 6.99749C18.6436 6.99749 18.3281 6.63802 18.3281 6.19636C18.3281 5.75469 18.6436 5.39523 19.0312 5.39523Z" fill="#051A52" />
                                                </g>
                                                <defs>
                                                    <clipPath id="clip0">
                                                        <rect width="24" height="27.3452" fill="white" transform="translate(0 0.535156)" />
                                                    </clipPath>
                                                </defs>
                                            </svg>
                                        </a>
                                    </li>

                                </ul>
                            </div >

                            <div className="col-md-3">
                                <h6 >Programs</h6>

                                {/* <!-- Nav Link --> */}
                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Data Analyst</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Business Analyst</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Product Design</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Product Manager</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Operations Manager in Tech</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Financial Advisory for Tech</a></li><li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Digital Transformation Expert</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Legal for Tech</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Engineering Leadership</a></li>
                                </ul>
                                {/* <!-- End Nav Link --> */}
                            </div>

                            <div className="col-md-3 ">
                                <h6 >USEFUL LINKS</h6>


                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">About us</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">FAQs</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Payment Plans</a></li>
                                </ul>

                            </div>

                            <div className="col-md-3 ">
                                <h6 >TERMS & CONDITIONS</h6>


                                <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Privacy Policy</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Cookie Policy</a></li>
                                    <li className="nav-item"><a className="nav-link nav-footer" href="front.html#">Payment Terms</a></li>
                                </ul>
                            </div>

                        </div >
                    </div >

                    <hr className="opacity-xs my-0" />
                    <div>
                        <p style={{ padding: "2rem 0" }}>
                            © 2021 Garden Academy - All Rights Reserved.
                        </p>
                    </div>

                </div >
            </footer >
            {/* <!-- ========== END FOOTER ========== --> */}

        </React.Fragment>
    );
}


export default AppFooter;