import { Field, Text, Textarea } from "@chakra-ui/react";
import {
  Control,
  Controller,
  FieldError,
  FieldValues,
  Path,
} from "react-hook-form";

interface Props<T extends FieldValues> {
  name: Path<T>;
  control: Control<T>;
  label: string;
  error?: FieldError;
  isDisabled?: boolean;
}

export const CustomTextArea = <T extends FieldValues>({
  name,
  control,
  error,
  label,
  isDisabled = false,
}: Props<T>) => {
  return (
    <div>
      <Field.Root>
        <Field.Label>{label}</Field.Label>
        <Controller
          name={name}
          control={control}
          render={({ field }) => {
            return (
              <Textarea
                id={name}
                {...field}
                disabled={isDisabled}
                className={`form-control ${error ? "is-invalid" : ""}`}
                value={field.value}
                onChange={field.onChange}
              />
            );
          }}
        />
        {error && <Text color="red.500">{error.message}</Text>}
      </Field.Root>
    </div>
  );
};
