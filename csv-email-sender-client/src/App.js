import "./App.css";
import Navbar from "./components/navbar";
import Header from "./components/header";
import PackageDetails from "./components/package-details";
import UserForm from "./components/user-form";

function App() {
  return (
    <div className="container">
      <Navbar />
      <Header />
      <div className="content-wrapper">
        <div>some image</div>
        <PackageDetails />
        <UserForm />
      </div>
    </div>
  );
}

export default App;
