import blog1 from "../assets/img/blog1.svg";
import blog2 from "../assets/img/blog2.svg";
import blog3 from "../assets/img/blog3.svg";
import blog4 from "../assets/img/blog4.svg";
import person from "../assets/img/person.svg"
import { Pagination } from "react-bootstrap";
export interface AppBlogProps {}

const AppBlog: React.SFC<AppBlogProps> = () => {
let active = 2;
let items = [];
for (let number = 1; number <= 5; number++) {
  items.push(
    <Pagination.Item key={number} active={number === active}>
      {number}
    </Pagination.Item>,
  );
}

const paginationBasic = (
  <div>
     
    <Pagination size="sm">{items}</Pagination>
  
  </div>
);

    return(
<>
<main>
    <div className="hero-page-about">
        <h1>Garden Academy Blog</h1>

        <p>
        Check out our Blog to stay up to date with great contents
        </p>
      </div>
      <br />
      <div style={{backgroundColor:'black'}}>
        <div className="container space-bottom-2" >
        <div className="row space-top-3 space-bottom-2">
          <div className=" col-lg-12">
            <div className="mb-4">
             
              <h1 className="white-text">What's New</h1>
            </div>

         
 </div>



            <div className="row col-lg-12 mx-n2 mx-lg-n3">
 <div
            className="col-lg-8"
            style={{ padding: "5%", position: "relative" }}
          >
            <img
              src={blog1}
              alt=""
              className="img-fluid img-data d-none d-lg-block"
            />
            <img src={blog2} alt="" className="img-fluid img-data d-lg-none" />

           
          </div>
         
          <div className="col-lg-4"  style={{ padding: "5%", position: "relative" }}>
            <div className="row mx-n2 mx-lg-n3" >
                <p className="white-text">CULTURE • January 11, 2021</p>
                <h3 style={{color:'#677788'}}>
                    Confidence and optimism the new norm in 2020
                </h3>
              <p className="white-text">
               Lorem ipsum dolor sit amet, consectetur vred adipiscing cresh tortor, pellentesque consectetur adipiscing benelit freesa accusantium des doloremque totam rem eaque ipsa quae ab illo inventore explicabo.
              </p>

            
            <p>
                 <div
                        className="row card-icon"
                        style={{ marginBottom: "0 !important" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ margin: "0px",color:"white" }}>
                            Patience Toyosi
                          </p>
                          <p style={{ fontSize: "12px", color: "#81909D" }}>
                          Founder & CEO
                          </p>
                        </div>
                      </div>
            </p>
            </div>
          </div>

         </div>
         <div className="row col-lg-12 mx-n2 mx-lg-n3" style={{justifyContent:"space-between"}}>

            <div className="row col-lg-6 mx-n2 mx-lg-n3">
 <div
            className="col-lg-12"
            style={{ padding: "5%", position: "relative" }}
          >
            <img
              src={blog3}
              alt=""
              className="img-fluid img-data d-none d-lg-block"
            />
            <img src={blog2} alt="" className="img-fluid img-data d-lg-none" />

           
          </div>
         
          <div className="col-lg-12"  style={{ padding: "5%", position: "relative" }}>
            <div className="row mx-n2 mx-lg-n3" >
                <p className="white-text">CULTURE • January 11, 2021</p>
                <h3 style={{color:'#677788'}}>
                    Confidence and optimism the new norm in 2020
                </h3>
              <p className="white-text">
               Lorem ipsum dolor sit amet, consectetur vred adipiscing cresh tortor, pellentesque consectetur adipiscing benelit freesa accusantium des doloremque totam rem eaque ipsa quae ab illo inventore explicabo.
              </p>

            
            <p>
                 <div
                        className="row card-icon"
                        style={{ marginBottom: "0 !important" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ margin: "0px",color:"white" }}>
                            Patience Toyosi
                          </p>
                          <p style={{ fontSize: "12px", color: "#81909D" }}>
                          Founder & CEO
                          </p>
                        </div>
                      </div>
            </p>
            </div>
          </div>

         </div>
         

            <div className="row col-lg-6 mx-n2 mx-lg-n3">
 <div
            className="col-lg-12"
            style={{ padding: "5%", position: "relative" }}
          >
            <img
              src={blog4}
              alt=""
              className="img-fluid img-data d-none d-lg-block"
            />
            <img src={blog2} alt="" className="img-fluid img-data d-lg-none" />

           
          </div>
         
          <div className="col-lg-12"  style={{ padding: "5%", position: "relative" }}>
            <div className="row mx-n2 mx-lg-n3" >
                <p className="white-text">CULTURE • January 11, 2021</p>
                <h3 style={{color:'#677788'}}>
                    Confidence and optimism the new norm in 2020
                </h3>
              <p className="white-text">
               Lorem ipsum dolor sit amet, consectetur vred adipiscing cresh tortor, pellentesque consectetur adipiscing benelit freesa accusantium des doloremque totam rem eaque ipsa quae ab illo inventore explicabo.
              </p>

            
            <p>
                 <div
                        className="row card-icon"
                        style={{ marginBottom: "0 !important" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ margin: "0px",color:"white" }}>
                            Patience Toyosi
                          </p>
                          <p style={{ fontSize: "12px", color: "#81909D" }}>
                          Founder & CEO
                          </p>
                        </div>
                      </div>
            </p>
            </div>
          </div>

         </div>
         </div>

         <div className="row col-lg-12 mx-n2 mx-lg-n3" style={{justifyContent:"space-between"}}>

            <div className="row col-lg-4 mx-n2 mx-lg-n3">
 <div
            className="col-lg-12"
            style={{ padding: "5%", position: "relative" }}
          >
            <img
              src={blog1}
              alt=""
              className="img-fluid img-data d-none d-lg-block"
            />
            <img src={blog2} alt="" className="img-fluid img-data d-lg-none" />

           
          </div>
         
          <div className="col-lg-12"  style={{ padding: "5%", position: "relative" }}>
            <div className="row mx-n2 mx-lg-n3" >
                <p className="white-text">CULTURE • January 11, 2021</p>
                <h3 style={{color:'#677788'}}>
                    Confidence and optimism the new norm in 2020
                </h3>
              <p className="white-text">
               Lorem ipsum dolor sit amet, consectetur vred adipiscing cresh tortor, pellentesque consectetur adipiscing benelit freesa accusantium des doloremque totam rem eaque ipsa quae ab illo inventore explicabo.
              </p>

            
            <p>
                 <div
                        className="row card-icon"
                        style={{ marginBottom: "0 !important" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ margin: "0px",color:"white" }}>
                            Patience Toyosi
                          </p>
                          <p style={{ fontSize: "12px", color: "#81909D" }}>
                          Founder & CEO
                          </p>
                        </div>
                      </div>
            </p>
            </div>
          </div>

         </div>
         

            <div className="row col-lg-4 mx-n2 mx-lg-n3">
 <div
            className="col-lg-12"
            style={{ padding: "5%", position: "relative" }}
          >
            <img
              src={blog1}
              alt=""
              className="img-fluid img-data d-none d-lg-block"
            />
            <img src={blog2} alt="" className="img-fluid img-data d-lg-none" />

           
          </div>
         
          <div className="col-lg-12"  style={{ padding: "5%", position: "relative" }}>
            <div className="row mx-n2 mx-lg-n3" >
                <p className="white-text">CULTURE • January 11, 2021</p>
                <h3 style={{color:'#677788'}}>
                    Confidence and optimism the new norm in 2020
                </h3>
              <p className="white-text">
               Lorem ipsum dolor sit amet, consectetur vred adipiscing cresh tortor, pellentesque consectetur adipiscing benelit freesa accusantium des doloremque totam rem eaque ipsa quae ab illo inventore explicabo.
              </p>

            
            <p>
                 <div
                        className="row card-icon"
                        style={{ marginBottom: "0 !important" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ margin: "0px",color:"white" }}>
                            Patience Toyosi
                          </p>
                          <p style={{ fontSize: "12px", color: "#81909D" }}>
                          Founder & CEO
                          </p>
                        </div>
                      </div>
            </p>
            </div>
          </div>

         </div>
         

            <div className="row col-lg-4 mx-n2 mx-lg-n3 ">
 <div
            className="col-lg-12"
            style={{ padding: "5%", position: "relative" }}
          >
            <img
              src={blog1}
              alt=""
              className="img-fluid img-data d-none d-lg-block"
            />
            <img src={blog2} alt="" className="img-fluid img-data d-lg-none" />

           
          </div>
         
          <div className="col-lg-12"  style={{ padding: "5%", position: "relative" }}>
            <div className="row mx-n2 mx-lg-n3" >
                <p className="white-text">CULTURE • January 11, 2021</p>
                <h3 style={{color:'#677788'}}>
                    Confidence and optimism the new norm in 2020
                </h3>
              <p className="white-text">
               Lorem ipsum dolor sit amet, consectetur vred adipiscing cresh tortor, pellentesque consectetur adipiscing benelit freesa accusantium des doloremque totam rem eaque ipsa quae ab illo inventore explicabo.
              </p>

            
            <p>
                 <div
                        className="row card-icon"
                        style={{ marginBottom: "0 !important" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ margin: "0px",color:"white" }}>
                            Patience Toyosi
                          </p>
                          <p style={{ fontSize: "12px", color: "#81909D" }}>
                          Founder & CEO
                          </p>
                        </div>
                      </div>
            </p>
            </div>
          </div>

         </div>
         </div>
       <div className="col-lg-12" style={{justifyContent:'center',display:"flex"}}>
{paginationBasic}
</div>
        </div>

        
      </div>
      
</div>
</main>
</>
    );
};
export default AppBlog;