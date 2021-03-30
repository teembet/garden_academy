let getMethods = async (url, token = null) => {
  let result = await fetch(process.env.REACT_APP_URL + url, {
    method: "GET",
    mode: "cors",
    cache: "no-cache",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
  });

  return await result.json();
};

let postMethods = async (url, params = {}, token = null) => {
  let result = await fetch(process.env.REACT_APP_URL + url, {
    method: "POST",
    mode: "cors",
    cache: "no-cache",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
    body: JSON.stringify({
      ...params,
    }),
  });

  return await result.json();
};

let getCourses = async (token) => {
  let result = await fetch(process.env.REACT_APP_COURSES, {
    method: "GET",
    mode: "cors",
    cache: "no-cache",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
  });

  return await result.json();
};

export { getMethods, postMethods, getCourses };
