
import { initializeApp } from "firebase/app";
import {getStorage} from 'firebase/storage';


const firebaseConfig = {
  apiKey: "AIzaSyBxxIXNYQqez_ogR2tqB5aGfwEydODh27Y",
  authDomain: "rookieecommerce-2ded0.firebaseapp.com",
  projectId: "rookieecommerce-2ded0",
  storageBucket: "rookieecommerce-2ded0.appspot.com",
  messagingSenderId: "1059542226591",
  appId: "1:1059542226591:web:22d177cc22fd23d80334aa"
};

const app = initializeApp(firebaseConfig);
export const imageDb = getStorage (app)