import { Link } from 'react-router-dom';
import '../assets/css/programs.css'
import { FaStar } from "react-icons/fa";
import { FaStarHalfAlt } from "react-icons/fa"
import pd1 from "../assets/img/pd1.svg";


export interface CourseCardProps {

}

const CourseCard: React.SFC<CourseCardProps> = () => {
    return (
        <>
            <Link to="/details">
                <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                    <div className="card" style={{
                        padding: "0", borderRadius: "4%", border: "1px solid #D7DCE0", boxSizing: "border-box"
                    }}>
                        <img className="img-fluid card-img-top" src={pd1} alt="product design" style={{ width: "100%" }} />
                        <div className="card-body">
                            <p className="product-title"><b>Product Design</b></p>
                            <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                            <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                            <p className="amount">NGN250,000</p>
                        </div>
                    </div>
                </div>
            </Link>
        </>
    );
}

export default CourseCard;