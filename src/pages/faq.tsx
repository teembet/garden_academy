import Search from "../components/search";
import { Card, Accordion } from "react-bootstrap";
import { FaChevronDown } from "react-icons/fa";
import { useState } from "react";
export interface AppFAQProps {}

const AppFAQ: React.SFC<AppFAQProps> = () => {
  const [faq, setFaq] = useState([
    { title: "What is Gaden academy?", tag: "1" },
    { title: "Why Gaden academy?", tag: "2" },
    { title: "How can I hire a talent?", tag: "3" },
    { title: "How do I make payment?", tag: "4" },
    { title: "Are there hidden charges when hiring a talent?", tag: "5" },
    { title: "How do you handle refunds?", tag: "6" },
    { title: "How do I unboard as a student?", tag: "7" },
    { title: "What are the payment plans available?", tag: "8" },
    { title: "Who can come on this platform?", tag: "9" },
    { title: "What are the benefits?", tag: "10" },
    { title: "What can I do on this platform?", tag: "11" },
    { title: "Why should I trust Garden academy?", tag: "12" },
  ]);

  const [faqStore, setFaqStore] = useState([
    { title: "What is Gaden academy?", tag: "1" },
    { title: "Why Gaden academy?", tag: "2" },
    { title: "How can I hire a talent?", tag: "3" },
    { title: "How do I make payment?", tag: "4" },
    { title: "Are there hidden charges when hiring a talent?", tag: "5" },
    { title: "How do you handle refunds?", tag: "6" },
    { title: "How do I unboard as a student?", tag: "7" },
    { title: "What are the payment plans available?", tag: "8" },
    { title: "Who can come on this platform?", tag: "9" },
    { title: "What are the benefits?", tag: "10" },
    { title: "What can I do on this platform?", tag: "11" },
    { title: "Why should I trust Garden academy?", tag: "12" },
  ]);

  const searchCourse = (searchInput: string) => {
    if (searchInput.trim() === "") return setFaq(faqStore);

    let courses = faqStore.filter((course: any) => {
      return course.title.toUpperCase().includes(searchInput.toUpperCase());
    });

    setFaq(courses);
  };

  return (
    <>
      <main>
        <div className="hero-page-about">
          <h1 className="d-none d-sm-block">Frequently Asked Questions</h1>
          <h4 className="d-block d-sm-none">Frequently Asked Questions</h4>

          <div className="row mt-3" style={{ width: "100%" }}>
            <div className="col-md-6 offset-md-3 ">
              <Search
                search={"What do you want to learn"}
                button_text={"Search"}
                onSearchSubmit={searchCourse}
                searchData=""
              ></Search>
            </div>
          </div>
        </div>
        <div className="offset-lg-2 col-lg-8 space-2">
          <Accordion defaultActiveKey="0">
            {faq.map((data, index) => {
              return (
                <Card key={index} style={{ border: "none", height: "auto" }}>
                  <Card.Header>
                    <h3 className="text-primary">{data.title}</h3>
                    <Accordion.Toggle
                      as={Card.Header}
                      variant="link"
                      eventKey={index.toString()}
                    >
                      <FaChevronDown />
                    </Accordion.Toggle>
                  </Card.Header>
                  <Accordion.Collapse eventKey={index.toString()}>
                    <Card.Body>
                      Lorem ipsum, dolor sit amet consectetur adipisicing elit.
                      Quidem, fuga rerum ea error nostrum placeat itaque
                      mollitia numquam libero veritatis fugit excepturi esse
                      iusto nobis sint est autem suscipit nemo?
                    </Card.Body>
                  </Accordion.Collapse>
                </Card>
              );
            })}
          </Accordion>
        </div>
      </main>
    </>
  );
};
export default AppFAQ;
