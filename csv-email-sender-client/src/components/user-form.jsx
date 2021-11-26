import { useState } from "react";
import axios from "axios";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
const UserForm = () => {
  const baseUrl = "https://localhost:44379/";

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [id, setId] = useState("");
  const [phone, setPhone] = useState("");
  const [email, setEmail] = useState("");
  const [city, setCity] = useState("");
  const [street, setStreet] = useState("");
  const [apartmentNum, setApartmentNum] = useState("");

  const MySwal = withReactContent(Swal);

  const sendEmail = async (e) => {
    e.preventDefault();

    console.log("data", {
      firstName,
      lastName,
      id,
      phone,
      email,
      city,
      street,
      apartmentNum,
    });
    const config = {
      headers: {
        "Content-Type": "application/json",
      },
    };

    axios
      .post(
        baseUrl + "api/userdata",
        { firstName, lastName, id, phone, email, city, street, apartmentNum },
        config
      )
      .then((res) => {
        console.log(res);

        MySwal.fire({
          title: <p>Hello World</p>,
          footer: "Copyright 2018",
          didOpen: () => {
            MySwal.clickConfirm();
          },
        }).then(() => {
          return MySwal.fire(<p>Email sent successfully!</p>);
        });
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <>
      <form onSubmit={sendEmail} className="userform">
        <div className="first-last-name">
          <input
            dir="rtl"
            id="input-lastname"
            type="text"
            name="user_lastName"
            placeholder="שם משפחה*"
            value={lastName}
            onChange={(e) => {
              setLastName(e.target.value);
            }}
          />
          <input
            dir="rtl"
            id="input-firstname"
            type="text"
            name="user_firstName"
            placeholder="שם פרטי*"
            value={firstName}
            onChange={(e) => {
              setFirstName(e.target.value);
            }}
          />
        </div>

        <input
          dir="rtl"
          id="id-num"
          type="text"
          name="user_idNum"
          placeholder="תעודת זהות*"
          value={id}
          onChange={(e) => {
            setId(e.target.value);
          }}
        />
        <input
          dir="rtl"
          id="phone"
          type="text"
          name="user_phone"
          placeholder="טלפון נייד"
          value={phone}
          onChange={(e) => {
            setPhone(e.target.value);
          }}
        />
        <input
          dir="rtl"
          id="email"
          type="email"
          name="user_email"
          placeholder="אימייל*"
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
          }}
        />

        <input
          id="city"
          type="text"
          name="user_city"
          placeholder="עיר"
          value={city}
          onChange={(e) => {
            setCity(e.target.value);
          }}
        />

        <div className="street-apartment">
          <input
            dir="rtl"
            id="apartmet-num"
            type="text"
            name="user_apartmentNum"
            placeholder="מספר"
            value={apartmentNum}
            onChange={(e) => {
              setApartmentNum(e.target.value);
            }}
          />
          <input
            dir="rtl"
            id="street"
            type="text"
            name="user_street"
            placeholder="רחוב"
            value={street}
            onChange={(e) => {
              setStreet(e.target.value);
            }}
          />
        </div>

        <input className="submitBtn" type="submit" />
      </form>
    </>
  );
};
export default UserForm;
