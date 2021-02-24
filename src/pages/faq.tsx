import Search from "../components/search";
import { Button, Card, Accordion } from "react-bootstrap";
import { FaChevronDown } from "react-icons/fa";
export interface AppFAQProps {}

const AppFAQ: React.SFC<AppFAQProps> = () => {
  return (
    <>
      <main>
        <div className="hero-page-about">
          <h1 style={{ fontSize: "48px", color: "#3A434B" }}>
            Frequently Asked Questions
          </h1>

          <div className="row mt-3" style={{ width: "100%" }}>
            <div className="col-md-6 offset-md-3 ">
              <Search
                search={"What do you want to learn"}
                button_text={"Search"}
                onSearchSubmit
                searchData=""
              ></Search>
            </div>
          </div>
        </div>
        <div className="offset-lg-2 col-lg-8 space-2">
          <Accordion defaultActiveKey="0">
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">What is Gaden academy?</h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="0">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="0">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>

            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">Why Garden Academy</h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="1">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="1">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">How can I Hire A Talent </h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="2">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="2">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">How do I make payment</h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="3">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="3">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">
                  Are there hidden charges when hiring a talents
                </h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="4">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="4">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">How do you handle refunds</h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="5">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="5">
                <Card.Body>Hello! I'm the body</Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">How do I onboard as a student</h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="6">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="6">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">
                  What are the payment plans available
                </h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="0">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="7">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">Who can come on this platform</h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="7">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="0">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">What are the benefits</h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="8">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="8">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
            <Card style={{ border: "none", height: "auto" }}>
              <Card.Header>
                <h3 className="text-primary">
                  Why should i trust Garden Academy
                </h3>
                <Accordion.Toggle as={Button} variant="link" eventKey="9">
                  <FaChevronDown />
                </Accordion.Toggle>
              </Card.Header>
              <Accordion.Collapse eventKey="9">
                <Card.Body>
                  Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                  Quidem, fuga rerum ea error nostrum placeat itaque mollitia
                  numquam libero veritatis fugit excepturi esse iusto nobis sint
                  est autem suscipit nemo?
                </Card.Body>
              </Accordion.Collapse>
            </Card>
          </Accordion>
        </div>
      </main>
    </>
  );
};
export default AppFAQ;
