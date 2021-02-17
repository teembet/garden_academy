import React from "react";
import "../assets/css/aboutpage.css";
import girl from "../assets/img/girl.png";
import Arlene from "../assets/img/Arlene.png";
import a from "../assets/img/a.png";
import b from "../assets/img/b.png";
import c from "../assets/img/c.png";
import d from "../assets/img/d.png";
import e from "../assets/img/e.png";
import f from "../assets/img/f.png";
import g from "../assets/img/g.png";
import h from "../assets/img/h.png";
import i from "../assets/img/i.png";
import j from "../assets/img/j.png";
import k from "../assets/img/k.png";
import study from "../assets/img/study.png";

export interface AppAboutUsPageProps {}

const AppAboutUsPage: React.SFC<AppAboutUsPageProps> = () => {
  return (
    <main id="content" role="main">
      <div className="hero-page-about">
        <h1>The Garden Academy Team</h1>

        <p>
          Choose from any of our wide range of courses tailored to suit your
          needs.
          <br /> From software engineering to product management and more, We
          have got you covered
        </p>
      </div>
      <br />
      <div className="container space-bottom-2">
        <div className="row space-top-3 space-bottom-2">
          <div className=" col-md-6">
            <div className="mb-4">
              <div className="top"></div>
              <h1>About Us</h1>
            </div>

            <br />

            <div className="row" style={{ margin: "0 5px" }}>
              <p>
                Lorem ipsum dolor sit amet, consectetur adipisicing elit. Fugit
                nam aliquid tenetur possimus dolorem hic mollitia saepe, itaque
                dolores vero dignissimos culpa facere id, corrupti fuga
                sapiente. Ea, maiores totam?
              </p>

              <p>
                Lorem, ipsum dolor sit amet consectetur adipisicing elit. Eaque,
                quia voluptate. Assumenda facere labore atque molestiae minus
                repellat? Repudiandae totam fugiat obcaecati dolore. Distinctio,
                quibusdam aliquam! Voluptate ipsa error natus?
              </p>
              <p>
                Lorem ipsum dolor sit amet, consectetur adipisicing elit. Fugit
                nam aliquid tenetur possimus dolorem hic mollitia saepe, itaque
                dolores vero dignissimos culpa facere id, corrupti fuga
                sapiente. Ea, maiores totam?
              </p>

              <p>
                Lorem, ipsum dolor sit amet consectetur adipisicing elit. Eaque,
                quia voluptate. Assumenda facere labore atque molestiae minus
                repellat? Repudiandae totam fugiat obcaecati dolore. Distinctio,
                quibusdam aliquam! Voluptate ipsa error natus?
              </p>
            </div>
          </div>

          <div
            className="col-md-6"
            style={{ padding: "5%", position: "relative" }}
          >
            <img
              src={girl}
              alt=""
              className="img-fluid img-data d-none d-lg-block"
            />
            <img src={study} alt="" className="img-fluid img-data d-lg-none" />

            <div className="box d-none d-lg-block"></div>
          </div>
        </div>

        <div className="container space-bottom-2">
          <div>
            <h1 style={{ textAlign: "center" }}>Meet Our Facilitators</h1>
            <p style={{ textAlign: "center" }}>
              We have carefully chosen a team of experienced and hardworking
              people in different fields <br />
              to train the next set of tech professionals.
            </p>
          </div>
          <br />
          <br />
          <div className="row">
            <div className="col-md-3">
              <img src={Arlene} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={a} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={b} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={c} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={d} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={e} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={f} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={g} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={h} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={i} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={j} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
            <div className="col-md-3">
              <img src={k} alt="" className="img-fluid img-data" />
              <br />
              <br />
              <h5>Arlene McCoy</h5>
              <p>Software Engineering Facilitator</p>
              <br />
            </div>
          </div>
        </div>

        <div className="action-box">
          <h2 className="text-primary" style={{ textAlign: "center" }}>
            Do you have what it takes to train the <br /> next set of tech
            superstars?
          </h2>

          <button type="submit" className="btn btn-primary transition-3d-hover">
            Become a Facilitator
          </button>
        </div>
      </div>
    </main>
  );
};

export default AppAboutUsPage;
