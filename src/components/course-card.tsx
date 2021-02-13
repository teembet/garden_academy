import { Link } from 'react-router-dom';
import '../assets/css/programs.css'
import { FaStar } from "react-icons/fa";
import { FaStarHalfAlt } from "react-icons/fa"
import pd1 from "../assets/img/pd1.svg";


export interface CourseCardProps {
    images: string,
    title: string,
    text: string,
    rating: string,
    price: string,
}

const CourseCard: React.SFC<CourseCardProps> = ({ images, title, text, rating, price }) => {
    return (
        <>

            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                <Link to="/details">
                    <div className="card" style={{
                        padding: "0", borderRadius: "4%", border: "1px solid #D7DCE0", boxSizing: "border-box"
                    }}>
                        <img className="img-fluid card-img-top" src={images} alt="product design" style={{ width: "100%" }} />
                        <div className="card-body">
                            <p className="product-title"><b>{title}</b></p>
                            <p className="products">{text}</p>
                            <p className="stars">{rating} <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                            <p className="amount">{price}</p>
                        </div>
                    </div>
                </Link>
            </div>

        </>
    );
}

export default CourseCard;