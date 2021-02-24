import { Link } from "react-router-dom";

import blog2 from "../assets/img/blog2.svg";
import blog3 from "../assets/img/blog3.svg";
import person from "../assets/img/person.svg";

export interface AppBlogSingleProps {}

const AppBlogSingle: React.SFC<AppBlogSingleProps> = (props: any) => {
  return (
    <>
      <main>
        {props.location?.state?.identity === "ui" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                User Experience Is the Most Important Metric You Aren't
                Measuring
              </h1>

              <p className="d-none d-sm-block">
                USER EXPERIENCE • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                User Experience Is the Most Important Metric You Aren't
                Measuring
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                USER EXPERIENCE • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Michael Georgiou
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        User experience is often overlooked in website and app
                        design and, indeed, the design of many things. How many
                        times have you felt compelled to push a door only to
                        find you need to pull it instead? While fire codes might
                        dictate such design, it’s an example of user experience
                        at work.
                        <br />
                        <br />
                        While taking a moment to figure out whether a door is
                        push or pull sounds like a small thing, those types of
                        irritants can add up online -- and cost your business
                        customers.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        What is UX?
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        The User Experience Professionals Association defines
                        user experience (UX) like this: “Every aspect of the
                        user’s interaction with a product, service, or company
                        that make up the user’s perceptions of the whole. User
                        experience design as a discipline is concerned with all
                        the elements that together make up that interface,
                        including layout, visual design, text, brand, sound and
                        interaction.”
                        <br />
                        <br />
                        In some cases, you can even equate UX to customer
                        service. Similarly, the Nielsen Norman Group says a good
                        user experience meets “the exact needs of the customer,
                        without fuss or bother.” But, to go above and beyond
                        with user experience means creating something that is “a
                        joy to use,” they said.
                        <br />
                        <br />
                        “True user experience goes far beyond giving customers
                        what they say they want or providing checklist
                        features," says Nielsen Norman Group. "In order to
                        achieve high-quality user experience in a company's
                        offerings, there must be a seamless merging of the
                        services of multiple disciplines, including engineering,
                        marketing, graphical and industrial design, and
                        interface design.”
                        <br />
                        <br />
                        Also notable in this description is that it applies to
                        any medium, whether it’s the design of a public
                        restroom, your company website or an airplane. Many
                        times, you see these terms in reference to online
                        design, and we’ll continue to use them in that sense
                        here.
                      </p>

                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        The difference between UX and UI.
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        We can’t discuss UX without mentioning user interface
                        (UI) because the two are often connected, and also
                        frequently confused. UX is science-based and takes into
                        account sociology. Think big picture.
                        <br />
                        <br />
                        User Interface is more graphically focused, with
                        attention to the buttons on which a user clicks and the
                        paths that follow. UI is the look and feel of a website,
                        its responsiveness and interactivity. A visitor might
                        have an excellent UI with your site but walk away with a
                        disappointing experience upon learning you don’t have
                        the content he or she is seeking.
                        <br />
                        <br />
                        Further, you’ll often hear about usability, another
                        essential element of your website and app design. This
                        refers to the utility of a site, whether it’s easy to
                        use and has the features you need. You may also see
                        references to this in terms of accessibility, providing
                        resources for people with disabilities. For example,
                        people with visual impairment rely on software that
                        reads alternative text out loud to them, describing
                        images on the screen.
                      </p>

                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Why UX matters.
                      </h3>
                      <br />
                      <br />

                      <p className="white-text">
                        Now that you have some idea of what UX means, why does
                        it matter? If you’re in business and trying to attract
                        customers via your website or app, it matters a lot.
                        Studies show:
                        <br />
                        <br />
                        If your content is not optimized, 79 percent of visitors
                        will leave and search again.
                        <br />
                        <br />
                        Mobile users are five times more likely to abandon a
                        task if your website is not optimized for their device.
                        (And two-thirds of mobile customers are looking to make
                        a purchase that day, so you do not want them to leave.)
                        <br />
                        <br />
                        ESPN.com revenues jumped 35 percent after they listened
                        to their community and incorporated suggestions into
                        their homepage redesign.
                        <br />
                        <br />
                        Eighty-eight percent of online consumers are less likely
                        to return to a site after a bad experience. According to
                        Adobe, 39 percent of people will stop engaging with a
                        website if images won’t load or take too long to load.
                        <br />
                        <br />
                        KoMarketing reports that 51 percent of people think
                        “through contact information” is the most critical
                        element missing from many company websites.
                        <br />
                        <br />
                        One study showed that a well-designed user interface
                        could raise your website’s conversion rate by up to a
                        200 percent, while better UX design could boost
                        conversion rates up to 400 percent.
                        <br />
                        <br />
                        The point is that your website is now akin to someone
                        walking into your store or office. If they encounter a
                        bad experience, don’t find what they need, or can’t
                        reach someone, they are going to leave and not come
                        back. You can also think of it this way: the more time,
                        money and effort you spend making your site fantastic
                        upfront, the less you’ll likely have to deal with bugs
                        and changes after its release. Google has indicated that
                        UX is one factor in search engine rankings. They want to
                        deliver results that customers want to land on so more
                        people use their search engine. Serving up a crummy site
                        is not going to satisfy searchers. Thus a site must be
                        easy to use, navigate and understand.
                      </p>

                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        How to create a great UX.
                      </h3>
                      <br />
                      <br />

                      <p className="white-text">
                        Many of the rules and guidelines out there are based on
                        research, but those indicators may not all apply to your
                        website or app. Of course, you should start with the
                        industry’s latest best practices, but the best way to
                        create a good user experience is to test your website or
                        app with users.
                        <br />
                        <br />
                        This process starts while your product is in
                        development. If you wait until the end, you may realize
                        too many problems to fix or that the overall approach is
                        not quite right. Test early, test often. Here are some
                        other tips when striving to build a good user
                        experience:
                        <br />
                        <br />
                        Build for the user. While obvious, sometimes it’s easy
                        to forget who is using your website or app. Something
                        may be beautiful, but that doesn’t mean it’s useful or
                        helpful to the user. That means letting go of some
                        ideas, even the ones you love.
                        <br />
                        <br />
                        Figure out who is your user. Skip the personas.
                        Categorize your users by their behavior on your website
                        or with your app. “Warm lead,” or “Just curious” might
                        be two users of a business website, for example. Less
                        really is more. Keep some white space. Keep it simple.
                        <br />
                        <br />
                        Ask for input from all departments. Your sales,
                        marketing, tech and design teams are all going to think
                        a bit differently about the website or app. Design by
                        committee can be tedious, but it’s OK to get a bit of
                        input at specific stages of the process.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "sell" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Sell More Products by Letting Your Customers Design Them for You
              </h1>

              <p className="d-none d-sm-block">
                SELLING MORE PRODUCT • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Sell More Products by Letting Your Customers Design Them for You
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                SELLING MORE PRODUCT • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Michael R. Solomon
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        When the founder of the beauty website Into the Gloss
                        decided to create a new cosmetics line, she didn't
                        contact vendors or post ads to entice new buyers.
                        Instead she created an Instagram account -- @glossier --
                        and waited for suggestions to flood in. She mined the
                        posts her followers submitted as she developed the line.
                        Thousands of cosmetics aficionados helped her to build
                        the new company.
                        <br />
                        <br />
                        We used to purchase goods and services from professional
                        artisans and manufacturers. Today it's also everyday
                        consumers who produce media content, design products and
                        invest in startups -- not to mention those who also act
                        as retailers, food critics, tour guides and even taxi
                        drivers. Consumer-generated content (CGC) wipes out the
                        traditional wall between producer and consumer. Everyday
                        people collaborate with, or even replace, professionals
                        in virtually every traditional marketing function. Let's
                        take a look at one of the most important ones, product
                        development.
                        <br />
                        <br />
                        DEWALT has an insight community of over 10,000 tool
                        users who submit ideas for new products. LEGO fans can
                        join LEGO Ideas, an online community where members
                        submit their own designs for new sets. They vote on
                        submissions -- if a project gets 10,000 votes, LEGO
                        considers it for an official LEGO Ideas set it will sell
                        around the world.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Co-creation is here!
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Call it co-creation, call it collaborative innovation,
                        call it crowdsourcing: The recruitment of consumers to
                        act as product designers is a growing trend. While this
                        teamwork can be threatening to highly trained designers
                        who fear they will be replaced by hordes of naïfs, the
                        less insecure ones recognize that user feedback can only
                        improve upon what they think people want.
                        <br />
                        <br />
                        The practice of handing the inmates the keys to the
                        asylum is booming. If you're doing this already, be sure
                        to fess up about where these innovations come from.
                        That's not just ethical practice; it turns out that
                        buyers actually prefer products users actually
                        suggested. One study found that crowdsourced products
                        sold up to 20 percent more when they were specifically
                        labeled as originating with customers. This helps to
                        explain why some brands are going out of their way to
                        trumpet this co-creation process. The German company Red
                        Chili that sells gear for rock climbers proclaims, "Only
                        climbers know what climbers need."
                      </p>

                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        The Maker Movement
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        The rapidly growing DIY (do it yourself) trend
                        represents another way that the wall between producers
                        and consumers is crumbling. Analysts project that this
                        market will grow by about 6 percent per year over the
                        next several years. One reason it's exploding is what
                        researchers call "The IKEA Effect": People prefer
                        products they help to make (or at least assemble).
                        <br />
                        <br />
                        The DIY craze is part of a larger trend some call the
                        Maker Movement. A makerspace is a collaborative
                        workspace inside a school, library or separate
                        public/private facility for creating, learning,
                        exploring and sharing. Typically a healthy dose of
                        entrepreneurship gets thrown in as well, as makerspaces
                        double as incubators for business startups. Some spaces
                        such as TechShop are themselves turning into businesses
                        as they expand the number of locations where DIYers can
                        access their sophisticated tools for a modest membership
                        fee. Makerspaces already are pouring out success
                        stories, such as the DODOcase company that uses a space
                        in San Francisco to build its popular line of covers for
                        phones and laptops.
                      </p>

                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Artisanal fries with that?
                      </h3>
                      <br />
                      <br />

                      <p className="white-text">
                        Artisanal cheese. Artisanal soap. Artisanal beer. The
                        term is everywhere. It implies that an item isn't
                        mass-produced, and often the maker is a skilled artist
                        who otherwise is "one of us" (i.e., he or she hasn't
                        sold out to a big corporation). The ecommerce site Etsy
                        calls itself "the most beautiful marketplace in the
                        world"; it features thousands of unique creations that
                        everyday people sell.
                        <br />
                        <br />
                        What's feeding the artisanal frenzy? Simply, the quest
                        for authenticity. Consumers today often want to know a
                        product's provenance; just where the things they buy
                        came from. The J. Peterman Company clothing catalogs
                        tell stories about the apparel they sell, and upscale
                        grocery stores like Whole Foods provide great detail
                        about the specific farms where produce and meat were
                        raised. I recently ate at a restaurant where the menu
                        actually listed the name of the specific fisherman who
                        caught the catch of the day! Product genealogy, or the
                        thirst to trace the backstory of a product from raw
                        materials to final form, is a popular activity for many.
                        <br />
                        <br />
                        The bottom line: Consumers crave authenticity. They love
                        companies that can boast of a long heritage and a
                        history of giving back to the communities where they
                        operate. If your company has a backstory, tell it.
                        Often.
                        <br />
                        <br />
                        The wall between producers and consumers is tumbling
                        down. Help your customers help you!
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "pmp" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                5 Phases of Project Management (PMP)
              </h1>

              <p className="d-none d-sm-block">PMP • January 11, 2021</p>

              <h4 className="d-block d-sm-none">
                5 Phases of Project Management (PMP)
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                PMP • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        ROLI PATHAK
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        There are different schools of thought about the number
                        of phases during a project. Some claim there are 3
                        phases; others say it’s 5. At the base of it, the PMBOK
                        points out that the number of phases is determined by
                        the project team and type of project.
                        <br />
                        <br />
                        Project management is solely based on the idea that a
                        project goes through a number a phases characterized by
                        a distinct set of activities or tasks that take the
                        project from conception to conclusion. Projects are big
                        and small, with constraints like cost, time, and
                        resources.
                        <br />
                        <br />
                        The 5 phases of project management include initiation,
                        planning, execution, monitoring, and project closure.
                        The Project Management Institute (PMI) originally
                        developed these five phases.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Project Initiation Phase
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        a project is formally started, named, and defined at a
                        broad level during this phase. Project sponsors and
                        other important stakeholders due diligently decide
                        whether or not to commit to a project. Depending on the
                        nature of the project, feasibility studies are
                        conducted. Or, as it may require, in an IT project –
                        requirement gathering and analysis are performed in this
                        phase. In the construction industry, a project charter
                        is completed in this phase.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Project Planning Phase
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        a project management plan is developed comprehensively
                        of individual plans for – cost, scope, duration,
                        quality, communication, risk and resources. Some of the
                        important activities that mark this phase are making
                        WBS, development of schedule, milestone charts, GANTT
                        charts, estimating and reserving resources, planning
                        dates, and modes of communication with stakeholders
                        based on milestones, deadlines, and important
                        deliveries. A plan for managing identified and
                        unidentified risks is determined as this may affect
                        aspects of a project later on. Risk management planning
                        includes: risk identification and analysis, risk
                        mitigation approaches, and risk response planning.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Project Execution Phase
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        a project deliverable is developed and completed,
                        adhering to a mapped-out plan. A lot of tasks during
                        this phase capture project metrics through tasks like
                        status meetings and project status updates, other status
                        reports, human resource needs, and performance reports.
                        This is an important phase, as it will help you
                        understand whether your project will be a success or
                        failure.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Project Monitoring and Control Phase
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        occurring at the same time as the execution phase, this
                        one mostly deals with measuring the project performance
                        and progression in accordance to the project plan. Scope
                        verification and control occur to check and monitor for
                        scope creep, and change of control to track and manage
                        changes to project requirement. Calculating key
                        performance indicators for cost and time are done to
                        measure the degree of variation, if any, and in which
                        case corrective measures are determined and suggested to
                        keep a project on track. To prevent project failure,
                        consider why projects are likely to fail and the ways to
                        prevent failure.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                          textDecoration: "underline",
                        }}
                      >
                        Project Closure Phase
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        A project is formally closed. It includes a series of
                        important tasks such as delivering the product,
                        relieving resources, rewarding team members, and formal
                        termination of contractors in case they were employed on
                        the project.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "best" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Best Project Management Software & Tools in 2021
              </h1>

              <p className="d-none d-sm-block">
                BEST PMP TOOL • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Best Project Management Software & Tools in 2021
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                BEST PMP TOOL • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Jose Maria Delos Santos
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        Nowadays, project management tools are expanding their
                        functions and crossing boundaries with their combination
                        of features, further complicating the user’s selection
                        process. We built a list of the best project management
                        software applicable for different types of industry and
                        business needs to assist in this crucial selection
                        process.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        1. Jira
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Jira is an agile project management software used by
                        development teams to plan, track, and release software.
                        It is a popular tool designed specifically and used by
                        agile teams. Aside from creating stories, planning
                        sprints, tracking issues, and shipping up-to-date
                        software, users also generate reports that help improve
                        teams, and create their own workflows. As part of
                        Atlassian, it integrates with many tools that enable
                        teams to manage their projects and products from end to
                        end. Jira Software is built for every member of your
                        software team to plan,track, and release great software.
                        Every team has a unique process for shipping software.
                        Use an out-of-the-box workflow, or create one to match
                        the way your team works.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        2. AceProject
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        AceProject is a web-based project tracking software that
                        helps manage projects from end to end. It is a complete
                        project management solution for individuals, teams and
                        enterprises that need to take control of their important
                        workflows and leave nothing to chance. AceProject
                        provides the tools for projects to remain on time and on
                        budget with its time and expense tracking features.
                        Entering time is very easy, almost automated, and
                        convenient with a Time Clock. Users can easily stay on
                        top of all their projects with a project Dashboard that
                        gives instant information with color-coded graphs and
                        details. With Gantt charts, they can view the
                        intricacies of a project and its progress to be able to
                        make informed decisions and necessary actions.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        3. Buildertrend
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Buildertrend is the #1 Software for home builders,
                        contractors & remodelers. Our construction software is
                        an all-in-one solution, it features everything you need
                        in one construction app.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        4. WorkflowMax
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Cloud-based worklow and job management software
                        delivered as Software-as-a-Service. It is an end-to-end
                        PM solution that has tools for leads, quotes,
                        timesheets, job management, and invoicing.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        5. BuildTools
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        BuildTools is a web-based, fully integrated construction
                        project management software. It is a modular
                        construction management platform designed to manage the
                        back-office processes of custom builders and remodelers.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        6. Procore
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Creates schedules, closes RFIs faster, tracks project
                        emails, archives documents & photos. Manage submittals,
                        daily logs, change orders, job costing and punch lists.
                        Integrates with MS Project and and Sage Timberline
                        Office.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        7. codeBeamer ALM
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        codeBeamer ALM is a market-leading collaborative Agile
                        Application Lifecycle Management platform that helps
                        manage complexity in software-heavy product development
                        projects. With out-of-the-box project management
                        functionality (Wikis, Gantt charts, Kanban boards,
                        customizable reporting dashboards & more), codeBeamer
                        helps align and unify the work of multiple (IT,
                        development, and operations) teams.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "measure" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Measuring Engagement is Not the Same as Listening
              </h1>

              <p className="d-none d-sm-block">
                MEASURING ENGAGEMENT • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Measuring Engagement is Not the Same as Listening
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                MEASURING ENGAGEMENT • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Sarah Johnson
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        The problem is that actual listening requires
                        organizations to ask more than a few
                        statistically-relevant questions on employee surveys.
                        But more than that, the survey fatigue isn’t because
                        employees are tired of multi-question surveys. They’re
                        tired of answering the same questions over and over and
                        seeing little action. They’re tired of not being
                        listened to.
                        <br />
                        <br />
                        This notion of Continuous Listening has become a bit of
                        a buzzword in HR circles. While Continuous Listening is
                        the topic of dozens of articles and presentations, there
                        is no single, clear, and agreed-upon definition of
                        "continuous listening." Continuous listening might mean
                        a high-frequency survey design, be it daily, weekly,
                        monthly. It could mean a combination of an annual
                        employee engagement surveys and lifecycle surveys, which
                        add the mixture of onboarding and exit surveys into the
                        mix. Special topic pulse surveys can be added in for
                        good measure. Continuous listening means Continuous
                        Data, an ongoing flow of survey responses from
                        employees. The data is from multiple points in time, and
                        may or may not include various topic areas and employee
                        subgroups.
                        <br />
                        <br />
                        Companies listen to their customers continuously, so why
                        not listen to the same way to employees? Customers are
                        asked for feedback after they have had a defined
                        interaction with a company. It might be a sale, it might
                        be a service call, or it might be any other experience
                        with the organization. The survey is designed to
                        understand the elements of that specific interaction and
                        allow for problem-solving.
                        <br />
                        <br />
                        Collecting data from employees continuously sounds like
                        a great idea and potentially valuable. And it can be,
                        assuming it is done correctly. So, what are some of the
                        potential mistakes that organizations need to avoid when
                        implementing continuous listening programs and improve
                        employee engagement?
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Choosing methodology before determining strategy
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Much of the attention given to Continuous Listening is
                        the focus it has placed on a method, rather than on how
                        the data are to be used. Implementing a
                        quarterly/monthly/daily survey without a clear strategy
                        for what is to be measured, why the data are needed, and
                        who will use the data is a recipe for failure. When it
                        comes to Continuous Listening, there is no
                        one-size-fits-all, no single design or methodology that
                        works for every company.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Collecting the wrong data
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        An hourly, daily, weekly, monthly, or annual feed of
                        data is useless if it isn't The Right Data. The Right
                        Data is illuminating, insightful, actionable, and
                        valuable. The wrong data are misleading, irrelevant,
                        confusing, and not actionable. It doesn't matter how
                        current the data are, or how frequently updates are
                        provided. If it isn't The Right Data, it is entirely
                        pointless. Continuous listening is most valuable when
                        the information needs of the organization drive it.
                        Organization strategy inevitably has people's
                        implications, whether the ability to retain critical
                        talent, reshape culture, manage global growth, or
                        communicate in new ways. Designing a listening program
                        around strategic initiatives enables HR staff to come to
                        the table with facts and data that allow senior leaders
                        to make the right data-driven decisions about people.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Too much data, too frequently
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Surveys that collect data daily or weekly or monthly or
                        even quarterly can produce an overwhelming amount of
                        data. For some companies, the continuous onslaught of
                        data can become a deterrent to taking action ("let's
                        wait another month/quarter to see if the trend holds
                        before we take action"). Carefully consider what data
                        needs to be collected continuously, and if the data
                        change very little over time, consider collecting data
                        less frequently.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        The failure to link datasets
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Companies collect scads of data, whether via HRIS or
                        surveys, training databases, succession plans, and more.
                        Most of the time, data from multiple sources are never
                        integrated, e.g., the datasets aren't connected and
                        cannot be analyzed together. Various groups in the
                        company collect and own their data, and each group
                        drives different and potentially conflicting actions
                        based on what their source of data tells them. The
                        relationships between data collected in these various
                        surveys are never explored or understood. Successful
                        companies use tools that integrate data from disparate
                        groups, allowing for analysis that links antecedents,
                        actions, policies, and outcomes to tell a story about
                        organization success.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Lack of ownership
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Every set of data needs a clear owner that is
                        responsible for strategy, analysis, interpretation,
                        sharing, and driving action. Without an owner, someone
                        accountable for acting, then very little or nothing gets
                        done. Owners may be HR Business Partners, staff groups,
                        senior leaders, and managers. Make sure the data owners
                        understand their role and the expectation for action.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Technology that isn't up to the task.
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Speed and quick turnaround of results are essential in
                        any organization. Given the technology available today,
                        there is no reason that organizations should wait for
                        months or even weeks for survey results. Real-time
                        survey technology enables not just quick turnaround of
                        results, but also the ability to analyze multiple data
                        streams together in virtually unlimited ways via live
                        queries of the data set.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Complex and confusing reporting.
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Data reports must be clear and straightforward.
                        Dashboards that direct the user to the most salient
                        issues and make the analysis process clear and
                        straightforward will encourage data usage. Nudge engines
                        and nudge technology can be used to present each user
                        with the data that is most essential for their
                        understanding of critical issues. Additional nudges can
                        be used to prompt further analysis, discussion, and
                        action.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        All data, no insight.
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        There is a big difference between collecting data and
                        listening. Organizations may have scads of data at their
                        disposal, but without analytics tools and capabilities,
                        there is no way to know what it all means. Data from
                        multiple sources require the ability and tools to pull
                        it all together, to synthesize all of these streams into
                        an integrated set of insights. It's like being in a
                        meeting where everyone is speaking at once. They may all
                        have useful ideas, but if you can't find a way to corral
                        all of that information, some way to sequence and
                        organize it, then it is just noise.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        All talk, no action
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        At the end of the day, it's easy to listen, and while
                        listening is excellent, employees want to see change.
                        Survey upon a survey that doesn't yield meaningful
                        change will sour employees to the listening process.
                        Employees don't become fatigued from answering
                        questions; they become fatigued when they spend all
                        their time responding, but leaders don't seem to care
                        enough to act.
                        <br />
                        The advancement of survey technology has enabled
                        organizations to collect just about any type of data
                        from any employee at any time. When anything is
                        possible, technologically, make sure the organization is
                        clear on what is most essential to the success of the
                        organization.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "emerging" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Emerging VR & AR in Recruitment - The Simulation process
              </h1>

              <p className="d-none d-sm-block">
                EMERGING VR • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Emerging VR & AR in Recruitment - The Simulation process
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                EMERGING VR • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Paul Osborne
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        Modern enterprises are increasingly opting for new
                        technologies for enhanced efficiency. Virtual Reality
                        (VR) and Augmented Reality (AR) have now grown popular
                        for employee training, recruitment, and several other HR
                        processes. Yes, those old days of finding resumes as the
                        only concrete document for employee evaluation has gone
                        with the wind of change. Now, a host of new
                        technologies, including VR and AR, are making this
                        possible most innovatively.
                        <br />
                        <br />
                        Recently, Lloyds Banking Group incorporated virtual
                        reality for the assessment process of the Graduate
                        Leadership Programme candidates. This VR program is
                        asking the recruits to solve a puzzle in a simulated
                        environment, and based on the results showcasing the
                        strengths and abilities of the recruits, and the company
                        could make recruitment decisions.
                        <br />
                        <br />
                        Let us explain here some of the time-tested ways VR and
                        AR-based simulation can help in the modern recruitment
                        process.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Helping Future Recruits to Experience the Workplace
                        Remotely
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        There are other ways VR and AR can make the recruitment
                        process easier. For example, during recruitment,
                        employees spend considerable time in the company's
                        workplace for evaluating the work environment and its
                        respective pros and cons. Now, a VR based simulated
                        environment of a workplace can help employees give a
                        better idea about the working environment they are going
                        to be part of.
                        <br />
                        <br />
                        Most importantly, this can give employees relief from
                        the compulsion of visiting the company's workplace
                        personally. While sitting miles away, they can get an
                        air of the working environment right through their
                        mobile device screen.
                        <br />
                        <br />
                        The recruitment process of Jet.com is a nice example of
                        this practice. By offering VR based immersive
                        presentations, the company is helping the recruits to
                        get a clear idea of the workplace they are going to be a
                        part of. The Walmart online shopping platform is also
                        utilizing VR experience to showcase the working
                        environment and experience of their workplace.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Making Use of Gamification for Job Applications
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Recruitment is a competitive field since it allows a
                        company to pull the best talents from the market and get
                        a competitive edge. Since every company is working hard
                        to get the best of the limited pool of talents in the
                        job market, using new and innovative ways to get in
                        touch with the right employees is essential.
                        <br />
                        <br />
                        In this respect, the use of gamification can be a good
                        way to replace the old application processes with
                        something efficient, new, and innovative. This will also
                        help boost the interactivity of the recruitment process
                        to a great extent. Instead of allowing the recruits to
                        fill up a long-form with all the details such as
                        educational qualification, experience, completed
                        projects, etc., a gamified process can reveal this
                        information through an interactive process.
                        <br />
                        <br />
                        This gamified process can particularly be useful for
                        recruitment screening. ActiView is an Israeli startup
                        that made use of VR technology to detect various
                        behavioral habits and attributes that are required for
                        evaluating the employees and their personalities.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Providing Technical Training to Employees
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Virtual Reality (VR) can help in orienting the employees
                        with various technical details related to their job
                        roles. VR can also help in providing an immersive
                        environment for new employee induction and training,
                        which is very much a part of the recruitment process.
                        Thanks to the use of VR, recruits can be better oriented
                        with their job tasks and get engaged with their job
                        roles faster.
                        <br />
                        <br />
                        Moreover, thanks to the immersive environment created
                        with VR, an employee can get hands-on training on
                        complex machines and technical parts right within a
                        training room. VR will be able to flee the boundaries
                        between reality and the virtual environment and help
                        faster learning and engagement.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Providing Safety Training
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        In many industries and enterprise environments, employee
                        safety is a constant matter of concern. In industries
                        where safety concerns are too many, virtual reality can
                        play an important role in safety training procedures. In
                        many industries where employees need to take commendable
                        risks for operating machines, handling equipment, and
                        raw materials, VR based safety training procedures can
                        train them better with procedures to ensure personal
                        safety and well-being.
                        <br />
                        <br />
                        This is particularly true for dealing with the safety
                        concerns of the production plants and facilities
                        involving heavy machinery, chemicals, and
                        life-threatening procedures and processes. For example,
                        in the firefighting industry, the VR based training on
                        new challenges corresponding to fire breakout incidents
                        can be hugely beneficial to curb accidental cases.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Providing Technical Training to Employees
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Virtual Reality (VR) can help in orienting the employees
                        with various technical details related to their job
                        roles. VR can also help in providing an immersive
                        environment for new employee induction and training,
                        which is very much a part of the recruitment process.
                        Thanks to the use of VR, recruits can be better oriented
                        with their job tasks and get engaged with their job
                        roles faster.
                        <br />
                        <br />
                        Moreover, thanks to the immersive environment created
                        with VR, an employee can get hands-on training on
                        complex machines and technical parts right within a
                        training room. VR will be able to flee the boundaries
                        between reality and the virtual environment and help
                        faster learning and engagement.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Providing Safety Training
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        In many industries and enterprise environments, employee
                        safety is a constant matter of concern. In industries
                        where safety concerns are too many, virtual reality can
                        play an important role in safety training procedures. In
                        many industries where employees need to take commendable
                        risks for operating machines, handling equipment, and
                        raw materials, VR based safety training procedures can
                        train them better with procedures to ensure personal
                        safety and well-being.
                        <br />
                        <br />
                        This is particularly true for dealing with the safety
                        concerns of the production plants and facilities
                        involving heavy machinery, chemicals, and
                        life-threatening procedures and processes. For example,
                        in the firefighting industry, the VR based training on
                        new challenges corresponding to fire breakout incidents
                        can be hugely beneficial to curb accidental cases.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "fintech" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Top 5 Fintech Trends That Will Shape Financial Markets in 2021
              </h1>

              <p className="d-none d-sm-block">
                FINTECH TRENDS • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Top 5 Fintech Trends That Will Shape Financial Markets in 2021
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                FINTECH TRENDS • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        James Jorner
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        The year 2020 was not the greatest for many industries
                        due to COVID-19. But interestingly, the likes of fintech
                        reported rapid growth during the pandemic. In the
                        absence of physical contacts, consumers relied on
                        virtual financial services to access and disburse funds
                        and fintech solutions came through.
                        <br />
                        <br />
                        The popularity of fintech has spiked in recent times
                        with 96% of global consumers admitting to being aware of
                        at least one fintech service.
                        <br />
                        <br />
                        Let us take a look at some fintech trends that are
                        projected to influence financial services in 2021.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        1. Autonomous finance
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Autonomous finance is on the top of the list of
                        outstanding fintech innovations. Juggling work with
                        utility bill payments, insurance, cable subscription,
                        etc., can be overwhelming. Autonomous finance takes the
                        burden off consumers’ shoulders and automates the
                        financial decision-making process with Artificial
                        Intelligence (AI) and Machine Learning. As more people
                        try to create more time for themselves, they will be
                        delegating recurring tasks to fintech solutions.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        2. Open banking
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Traditional banks are most notable for safeguarding
                        people’s money. With growing awareness of financial
                        education, more people want to invest their money rather
                        than keep it in the bank. Third-party financial
                        institutions are giving traditional banks a run for
                        their money in offering flexible high-income-generating
                        investments and consumers are keying into it via open
                        banking. Open banking gives third-party financial
                        service providers access to consumer banking data via
                        Application Programming Interfaces (APIs) for investment
                        purposes consented by the consumers.
                        <br />
                        <br />
                        Although there are security concerns over the exposure
                        of consumers' data in open banking, damages can be
                        prevented with the collaborative efforts of the parties
                        involved.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        3. Digital-only banks
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Long queues at the bank are a pain in the neck for most
                        consumers. Despite the provision of online banking,
                        there are still queues at the bank due to the
                        limitations of the online services.
                        <br />
                        <br />
                        The total eradication of physical contacts for banking
                        transactions seemed far-fetched until the pandemic hit.
                        Accessing funds virtually became a survival need that
                        conventional banking could not meet completely. A
                        McKinsey study showed that digital payment is one of the
                        biggest fintech products. New generation financial
                        institutions rose to the occasion by leveraging fintech
                        solutions to offer convenient digital-only banking
                        services that required no physical contact. The growing
                        competition among financial institutions in offering
                        digital-only banking services is good news to consumers
                        as they have an array of enticing offers to choose from.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        4. Financial literacy
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        The consumer's financial literacy level influences their
                        finances either positively or negatively. According to a
                        Bankrate report, an average American household has
                        $8,863 in bank or credit union savings. Younger people
                        and singles have lesser savings. In the same vein, 55%
                        of respondents in a recent study revealed that they lack
                        sufficient funds for their needs. The situation would
                        most likely be different if consumers were better
                        informed about their finances.
                        <br />
                        <br />
                        Fintech solutions are effective tools for financial
                        literacy. With the collection of big data, consumers
                        with bad finances can learn from those that have their
                        finances sorted. There are fintech tools to guide
                        customers with basic financial education in making
                        prudent financial decisions.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        5. Voice technologies
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Convenience is a watchword in fintech and creators in
                        the sector are keen on giving consumers the best there
                        is. Gen Zs are on the frontlines of technology trends.
                        Products that they find appealing become instant
                        successes and fintech is tapping into that trend to make
                        its solutions more attractive with the introduction of
                        voice technology. The youngsters who have a penchant for
                        chatting are gravitating toward voice-based tools in
                        their online interactions. AI-powered fintech voice
                        assistants offer convenience and simplicity in handling
                        finance-related tasks. Voice technology also advances
                        secure payments with the use of biometric data for
                        payment authorization.
                        <br />
                        <br />
                        Opportunities in fintech are endless as the innovation
                        thrives on the ever-evolving technology. Consumers want
                        to do more in their finances and fintech solutions are
                        rising to the occasion. With a track record of offering
                        useful financial information, payment security, speedy
                        and transparent transactions among others, fintech
                        trends are fast becoming the standard in financial
                        markets.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "artificial" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Using Artificial Intelligence to Improve Law Firm Performance
              </h1>

              <p className="d-none d-sm-block">
                ARTIFICIAL INTELLIGENCE • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Using Artificial Intelligence to Improve Law Firm Performance
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                ARTIFICIAL INTELLIGENCE • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Holly Urban
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        The evolution and utilization of artificial intelligence
                        (AI) is on the rise and shows no signs of stopping in
                        the near future. In fact, Statista reports that global
                        revenues from enterprise applications making use of AI
                        are expected to increase by almost $30B by 2025. With
                        such monumental growth, it’s no wonder the legal
                        industry is getting in on the action.
                        <br />
                        <br />
                        As luck would have it, the legal field is fertile ground
                        for the benefits of AI technology. Time-consuming tasks
                        that attorneys have previously expended manual resources
                        into can now be accomplished using automation and
                        machine-learning in less time and for less money.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        The Current State of AI Technology
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Artificial intelligence is a useful tool becoming more
                        widely used in real-world applications that learn to
                        complete tasks ordinarily done by humans.
                        <br />
                        <br />
                        Even for geniuses, it is unrealistic for lawyers to
                        maintain a complete catalog of everything they will ever
                        need to know in their heads at all times. That said,
                        having access to every piece of relevant data on the
                        procedure and outcome of a previous matter can be a huge
                        advantage in producing favorable outcomes in similar
                        matters in the future. That is where AI comes in.
                        <br />
                        <br />
                        A Harvard Law report notes, “because AI can access more
                        of the relevant data, it can be better than lawyers at
                        predicting the outcomes of legal disputes and
                        proceedings, and thus helping clients make decisions.
                        For example, a London law firm used data on the outcomes
                        of 600 cases over 12 months to create a model for the
                        viability of personal injury cases. Indeed, trained on
                        200 years of Supreme Court records, an AI is already
                        better than many human experts at predicting SCOTUS
                        decisions.”
                        <br />
                        <br />
                        AI also has various other–more
                        straightforward–applications of which law firms can take
                        advantage. Instead of spending work-hours completing
                        tedious and rote tasks, AI can do them for you.
                        Moreover, AI can usually complete tasks more
                        efficiently, thereby improving productivity and
                        standardization.
                        <br />
                        <br />
                        This increase in productivity allows your firm to
                        reallocate resources as necessary to even further
                        optimize efficiency, cut down on redundancy, and focus
                        on growing profits.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        In Practice
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        These improvements are not simply theoretical. According
                        to a recent article in Information Age, AI “is not a
                        totally new phenomenon, and the legal industry has been
                        using AI in the litigation discovery process for nearly
                        10 years.”
                        <br />
                        <br />
                        In fact, AI has already made its way into the legal
                        profession in the form of legal research, contract
                        review, and management, document review, predicting
                        legal outcomes, and more.
                        <br />
                        <br />
                        The rise of e-discovery is likely the earliest example
                        of the use of AI in the legal profession. With
                        everything organized in electronic form, AI allows
                        litigators to organize, thread, batch, and search for
                        relevant information in vastly more efficient ways than
                        the manual review of paper documents would allow.
                        <br />
                        <br />
                        Legal research has also been shown to be greatly aided
                        by AI. For example, a New York Times report chronicles
                        an experiment by a Miami based litigator who decided to
                        test the usefulness of legal research software. The
                        software (Ross Intelligence) is designed to search a
                        massive database of case law and produce the data most
                        relevant to that specific search. This attorney wanted
                        to see if he could find a case relevant to a matter with
                        which he was involved faster than the software. It took
                        him around 10 hours to find the case for which he was
                        looking. The software found the same case immediately.
                        Furthermore, according to Bloomberg Law, AI “helps legal
                        researchers unearth documents that they could not have
                        found previously and more easily identify similarities
                        between court opinions. Built over five years across 13
                        million court opinions and counting, this application of
                        AI can minimize the number of errors or missed documents
                        that a user might face.”
                        <br />
                        <br />
                        This translates to the ability to parse through and
                        analyze millions of legal data points, efficiently
                        organized by relevant criteria, to help forecast a
                        matter’s outcome and expenses–all with the push of a few
                        buttons. AI can provide you with information you
                        wouldn’t have even known to look for.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Perceived Impediments
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        A recent Deloitte Insight report has shown, in the legal
                        space specifically, “technology has already contributed
                        to the loss of more than 31,000 jobs in the sector but
                        that there has been an overall increase of approximately
                        80,000, most of which are higher skilled and better
                        paid.”
                        <br />
                        <br />
                        “Eliminating jobs” at first glance seems like a
                        negative. But, when the jobs being eliminated account
                        for high-turnover and produce low job satisfaction, it
                        is a clear benefit. This makes room to cultivate
                        higher-skilled positions and increase employee value in
                        ways unrealistic without AI shouldering some of the
                        burdens.
                        <br />
                        <br />
                        Another concern takes the form of confidentiality and
                        cybersecurity – and rightfully so. A recent study by a
                        malpractice insurer revealed that 22% of law firms were
                        the victims of cyberattacks. The victims were bigger
                        names in the field than you might expect, but smaller
                        firms are by no means exempt. For example, the American
                        Bar Association recently found that this figure was 35%
                        in law firms with 10-49 attorneys—meaning over a third
                        of small law firms had been hacked.
                        <br />
                        <br />
                        However, far from being a liability, CSO reports that AI
                        provides additional support in combating the constant
                        threat of cyberattacks. The self-learning algorithms
                        incorporated into AI technology allow it to better
                        understand and predict potential threats in ways that
                        humans often cannot.
                        <br />
                        <br />
                        In fact, according to an article in Law Technology
                        Today, AI implementation accounts for a decrease in
                        cybersecurity risk, as opposed to firms that maintain
                        outdated technology.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Conclusion
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        The combination of natural human-pattern recognition and
                        the support of the self-learning capabilities of AI
                        allow lawyers the ability to extricate pertinent
                        information faster and more easily than ever before.
                        <br />
                        <br />
                        Aside from the practical benefits the technology
                        provides, the ABA Model Rules of Professional Conduct
                        reiterate the possible ethical implications of making
                        use of available technology to assist clients, stating:
                        “To maintain the requisite knowledge and skill, a lawyer
                        should keep abreast of changes in the law and its
                        practice, including the benefits and risks associated
                        with relevant technology, engage in continuing study and
                        education and comply with all continuing legal education
                        requirements to which the lawyer is subject.”
                        <br />
                        <br />
                        The adoption of AI technology is currently a means of
                        gaining advantages of increased productivity and
                        efficiency. As the technology evolves, it may not be
                        long before it becomes an ethical obligation as a tool
                        there is no good reason not to use.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "habit" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Digital transformation: 11 habits of successful teams
              </h1>

              <p className="d-none d-sm-block">
                DIGITAL TRANSFORMATION • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Digital transformation: 11 habits of successful teams
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                DIGITAL TRANSFORMATION January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Stephanie Overby
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        Every technology leader knows that transformation is
                        difficult, and digital transformation is especially so.
                        While nearly all IT leaders (93 percent) recently
                        surveyed by Hanover Research said that their enterprises
                        are undergoing some kind of digital transformation,
                        almost half of them (42 percent) indicated that they are
                        struggling to achieve success as they fall behind
                        schedule or their efforts stall altogether.
                        <br />
                        <br />
                        Those teams with a better track record of success for
                        the digital initiatives, however, offer a blueprint for
                        other teams seeking to increase the likelihood that
                        their digital investments will deliver returns.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        11 things successful digital transformation leaders do
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        We asked IT leaders and digital transformation experts
                        to share some things that successful teams and their
                        leaders do well, so you could learn from their
                        experience. Consider how these apply in your
                        organization and where you need to focus the most:
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        1. Seek progress rather than perfection
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Perfection is the enemy of the good. And that certainly
                        holds true when it comes to digital transformation.
                        “Solutions, requirements, and technologies change so
                        fast during digital transformations, leaders trying to
                        deploy every requirement end up never moving forward,”
                        says Pace Harmon managing director Andrew Alpert. “Set
                        your base requirements with the understanding that you
                        will learn and evolve over time."
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        2. Quantify success and track it
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Articulate the value that digital initiatives are
                        expected to deliver. “The greatest hurdle digital
                        transformation efforts face is neither the technology
                        nor its implementation,” says Prashant Kelker, partner
                        for digital strategy & solutions at global technology
                        research and advisory firm ISG. “Rather, it’s the
                        ability to clearly define and predict the value being
                        created coupled with a way to track that value
                        accurately.”
                        <br />
                        <br />
                        As much as possible, digital transformation leaders
                        should directly correlate inputs to outcomes. “That is
                        where enterprises often fall short,” says Kelker, noting
                        that there is increasing discussion of new revenue
                        generation enabled by digital. “Almost 60 percent of
                        organizations are actually using digital transformation
                        to optimize operations,” Kelker adds.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        3. Say no
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Leaders succeeding with digital initiatives take hard
                        lines. “They keep the scope manageable,” Alpert says,
                        “and refrain from allowing additional requirements to
                        expand beyond the project objectives and increase the
                        overall risk to a successful implementation."
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        4. Be transparent
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        “In order for digital transformation to be successful,
                        your team must operate with complete transparency," says
                        Jay Ferro, CIO at Quikrete. "Communication needs to be
                        consistent and flow up, down, and across the
                        organization – whether it’s good, bad, or just
                        informational. When bad news needs to be communicated,
                        your team should feel empowered to deliver it without
                        fear of repercussions.
                        <br />
                        <br />
                        "CIOs and IT leaders can help create this culture
                        through consistent celebration of organizational ‘wins,’
                        as well as rallying around the ‘losses’ with a positive
                        focus – and figuring out how to fix them together,”
                        Ferro says.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        5. Encourage teams to be agile – in every way
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        “Teams will need to be agile far beyond the concept of
                        software development,” says Cecilia Edwards, partner at
                        Everest Group. “Given the accelerated rate of change in
                        digital transformations (and especially during this time
                        of COVID-19), no one can predict the specifics of what
                        will be needed even a year from now.”
                        <br />
                        <br />
                        Successful digital teams shore up foundational elements
                        that support most initiatives and then implement
                        transformation in stages to meet current requirements.
                        “They then need to constantly assess the requirements
                        and make adjustments,” Edwards says.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        6. Prioritize data governance
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        "Digital transformation success requires data governance
                        to ensure processes and leadership commitments are
                        aligned with the transformation,” Alpert says. Often, IT
                        leaders will establish a data council of stakeholders
                        and also institute streamlined processes to ensure
                        corporate data is accurate and the source of truth is
                        maintained, Alpert says.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        7. Proceed incrementally
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        “Big bang digital transformation strategies don’t work
                        anymore; organizations need to be ready to adapt and
                        make incremental change in response to ever-changing
                        market dynamics,” says Sherri Brouwer, senior director
                        of industry solutions at IT consulting and services
                        company Avanade.
                        <br />
                        <br />
                        Successful teams embrace the "test, iterate, and learn"
                        mindset across small and large products – uncovering new
                        business opportunities, revenue streams, and changes to
                        business processes. “Companies have to rethink now and
                        really focus on digital transformation. Being
                        disciplined about investing in what matters to the
                        business and stopping everything else can create
                        entirely new experiences and business results,” Brouwer
                        says.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        8. Insist on cross-functional approach
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        “Silos no longer work in a world of digital
                        transformation,” Edwards says. Technology people and
                        business people need to come together as one to drive
                        toward a common business goal. “This means that those
                        with IT roles must have a greater understanding of the
                        business and those with business roles must become more
                        versed in the capabilities technology brings,” Edwards
                        says.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        9. Seek to be a constantly running innovation engine
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        “Having sporadic ideas every so often is not enough,”
                        says John Castleman, CEO of digital consultancy
                        Mobiquity. “The key that leads to true transformation is
                        having a constant churn of ideas that will help
                        companies identify solutions.”
                        <br />
                        <br />
                        Those ideas are best focused on specific friction points
                        in the value chain. “Digital transformation leaders must
                        understand that creating an innovative culture is a
                        precondition to success. Encouraging cross-departmental
                        collaboration will lead to more holistic ideas that
                        solve for the friction points from all sides,” Castleman
                        says. “Teams should also be sure to keep ideas in
                        perspective and process the best ideas at predictable
                        costs to make it easier to select winning ideas and
                        scale quickly while maintaining a realistic budget.”
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        10. Approach change systematically
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Everybody wants change, Kelker says. But nobody wants to
                        be changed. “Change resistance is a real and often
                        subtle challenge to digital transformation. It’s not
                        about launching ‘feel-good’ workshops and innovation
                        theater.”
                        <br />
                        <br />
                        Analyzing processes and introducing gradual changes that
                        show results eases users into the change is a better
                        approach. “You don’t force the team to change,” Kelker
                        explains. “You enable it to change itself.”
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        11. Put customer needs ahead of technology
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        IT leaders should know this one well already, but let’s
                        re-emphasize it, since certain people in your
                        organization may be wowed a little too much by a
                        particular technology. “Successful businesses take time
                        to develop a clear understanding of their customers’
                        needs and the holes in the value chain that this
                        technology will solve for,” Castleman says. “After
                        establishing the end goals, teams can work backwards to
                        develop the technology that will most effectively
                        transform pain points for their customers.”
                        <br />
                        <br />
                        The best teams involve their customers in their
                        development and delivery process to validate assumptions
                        and test solutions. “Allowing customers to be involved
                        will also demonstrate a commitment to the advancement of
                        your industry, rather than just your company,” Castleman
                        says. “Producing truly innovative digital tools can
                        promote cross-industry open innovation initiatives to
                        attract ideas, talent, and potential partners.”
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : props.location?.state?.identity === "payment" ? (
          <div>
            <div
              className="hero-page-about"
              style={{ padding: "0 5%", textAlign: "center" }}
            >
              <h1 className="d-none d-sm-block">
                Three ways payment orchestration improves financial
                reconciliation
              </h1>

              <p className="d-none d-sm-block">
                PAYMENT ORCHESTRATION • January 11, 2021
              </p>

              <h4 className="d-block d-sm-none">
                Three ways payment orchestration improves financial
                reconciliation
              </h4>

              <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
                PAYMENT ORCHESTRATION • January 11, 2021
              </p>
            </div>

            <div style={{ backgroundColor: "black" }}>
              <div className="container space-bottom-2">
                <div className="row space-top-3 space-bottom-2">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <img
                      src={blog3}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </div>

                  <div
                    className="row space-top-1 "
                    style={{ marginBottom: "0 !important", width: "100%" }}
                  >
                    <div className="col-1">
                      <img
                        className="avatar img-fluid"
                        src={person}
                        alt="avatar"
                      />
                    </div>
                    <div className="col-8">
                      <p style={{ margin: "0px", color: "white" }}>
                        Brian Coburn
                      </p>
                    </div>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        When Luca Pacioli, the 15th century Venetian monk,
                        invented double-entry account keeping, managing
                        financial reconciliations had its own unique challenges.
                        The father of modern accounting didn’t have to deal with
                        glitches in his book-keeping app but he did have to
                        write with feather-based quills by candlelight. Five
                        hundred years later the challenges are different but no
                        less onerous.
                        <br />
                        <br />
                        As in the 15th century, solid financial reporting is at
                        the heart of every successful high-transaction business.
                        As Pacioli no doubt knew, up-to-date, well-documented
                        accounting ensures good operational health and makes it
                        easier to grow. And that’s never been more important.
                        <br />
                        <br />
                        While it might not be feather quills by moonlight,
                        today’s environment of multiple customer channels can be
                        time-consuming and labor intensive, with various payment
                        methods and financial reconciliations from multiple data
                        sources.
                        <br />
                        <br />
                        Understanding cash inflow through online transactions is
                        a critical element of financial reporting. However, when
                        these involve multiple payment processors and payment
                        methods and a complex system of disjointed silos of
                        payment data, this can become a cumbersome and arduous
                        manual task.
                        <br />
                        <br />
                        Common issues in this fragmented payment landscape
                        include working across different formats, managing
                        different data owners and access as well as inconsistent
                        process timings. The result is often increased
                        inaccuracy and inefficiency. Procuring multiple tools
                        and software can end up being uncost-effective and
                        unwieldy. Though the current digital transformation is
                        an exciting time for retailers, staying on top of the
                        ever-changing payment options can be an overwhelming
                        burden for many business owners. Introducing payment
                        orchestration presents a single, accessible, creative
                        and accurate source of transactional data, crucial for
                        today’s complex challenges around financial
                        reconciliations.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Simplicity
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Today, commerce is 24/7, so being able to access and
                        analyses real-time information is vital to managing
                        business controls. Many organizations have looked to
                        automate these processes with account reconciliation
                        software.
                        <br />
                        <br />
                        However, one key challenge is the sheer volume of
                        transactions and the need to capture data from a variety
                        of different sources. Payment orchestration enables
                        transactions to be carried out by multiple payment
                        processors and payment methods with simple and flexible
                        plugins, centrally monitored and routed in the most
                        optimum way.
                        <br />
                        <br />
                        It allows users to add or remove providers easily,
                        knowing the complexity (detecting outages and
                        automatically rerouting payments) is being handled by a
                        trusted specialist partner via an intelligent platform.
                        <br />
                        <br />
                        Bringing disparate sources of online transaction data
                        into one place simplifies how enterprises access and
                        operate with multiple payment processors and payment
                        methods. This makes it easier for businesses to remain
                        agile.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Speed
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        For organizations that still depend on manual,
                        spreadsheet driven processes, the mechanics of
                        reconciliation can be extremely time consuming.
                        <br />
                        <br />
                        A payment orchestration layer creates the opportunity to
                        automate processes and reduce manual intervention. By
                        bringing multiple payment processors and payment methods
                        into an integrated service layer with intelligent
                        routing capabilities, the impact of individual outages
                        or failed payments can be mitigated to ensure optimum
                        payment success rates, saving crucial revenue.
                        <br />
                        <br />
                        Although there are security concerns over the exposure
                        of consumers' data in open banking, damages can be
                        prevented with the collaborative efforts of the parties
                        involved.
                      </p>
                      <br />
                      <br />
                      <h3
                        style={{
                          color: "#82dec9",
                        }}
                      >
                        Accuracy
                      </h3>
                      <br />
                      <br />
                      <p className="white-text">
                        Naturally, significant manual work brings with it the
                        added risk of human error. The speed with which business
                        moves today demands accurate accounting processes.
                        Checking for error takes up valuable time that could be
                        spent focusing on business growth.
                        <br />
                        <br />
                        Payment orchestration can improve accuracy and reduce
                        the opportunity for error. Providing a holistic and
                        central source of real-time transactional data, payment
                        orchestration can offer improved transparency and
                        greater visibility of financial data.
                        <br />
                        <br />
                        With all transactional data captured in one source,
                        payment orchestration can present a data source to feed
                        other applications – such as automated reconciliation
                        tools and fraud management – automating business
                        processes in a seamless way across the enterprise. Good
                        practice like this will, of course, enable a consistent
                        approach to fraud management across all channels and
                        payment services.
                        <br />
                        <br />
                        Multiple payment choices can be onerous but, today, not
                        adopting them at all is unwise. The key to success, and
                        good financial reconciliation, is being able to
                        streamline and manage them.
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : (
          <div className="d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
            <div className="row" style={{ width: "100%" }}>
              <div className="col-6 offset-3">
                <h1 style={{ textAlign: "center" }}>Page Not Found</h1>
                <Link to="/" className="btn btn-primary btn-block">
                  Go to Home
                </Link>
              </div>
            </div>
          </div>
        )}
      </main>
    </>
  );
};

export default AppBlogSingle;
