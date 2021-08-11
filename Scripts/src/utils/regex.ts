export const regex = {
  // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:
  password:
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
};
